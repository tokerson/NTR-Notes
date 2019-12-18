using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [EnableCors("MyPolicy")]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private HashSet<string> allCategories = new HashSet<string>() { "All" };
        private int pageSize = 3;

        private readonly ILogger<NotesController> _logger;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int? page, string category, string startDate, string endDate)
        {
            using(var context = new NTR2019ZContext()) {
                var notes = await context.Note.Include(n => n.NoteCategory).ThenInclude(nc => nc.IdcategoryNavigation).ToListAsync();
                if(category != null && category != "All") {
                    page = 1;
                    notes = notes.Where(n => n.NoteCategory.Any(nc => nc.IdcategoryNavigation.Name.Equals(category))).ToList();
                }
                if(startDate != null && startDate != "Invalid date" && startDate != "null") {
                    page = 1;
                    notes = notes.Where(n => n.Date >= Convert.ToDateTime(startDate)).ToList();
                }
                if(endDate != null && endDate != "Invalid date" && endDate != "null") {
                    page = 1;
                    notes = notes.Where(n => n.Date <= Convert.ToDateTime(startDate)).ToList();
                }
                var categories = await context.Category.ToListAsync();
                var domainNotes = notes.Select(note => DomainModel.Note.Create(note)).AsQueryable();
                var pageOfNotes = PaginatedList<DomainModel.Note>.Create(domainNotes, page ?? 1, pageSize);
                return Ok(new { pageOfNotes=pageOfNotes, categories=categories.Select(c => c.Name), pager= new {currentPage=pageOfNotes.PageIndex, endPage=pageOfNotes.TotalPages }});
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int? id)
        {
            if (id == null) {
                return NotFound();
            }

            using(var context = new NTR2019ZContext()) {
                var note = await context.Note.Where(n => n.Idnote == id).Include(n => n.NoteCategory).ThenInclude(nc => nc.IdcategoryNavigation).FirstOrDefaultAsync();
                // return await PaginatedList<Note>.CreateAsync(notes, 1, pageSize);
                return Ok(new { data= DomainModel.Note.Create(note)});
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody]DomainModel.Note note)
        {
            using(var context = new NTR2019ZContext()) {
                if (context.Note.Any(n => n.Title == note.Title)) {
                    return BadRequest();
                }

                Note newNote = new Note(){
                    Title=note.Title,
                    Description=note.Description,
                    Date=note.Date,
                    IsMarkdown=note.IsMarkdown,
                };
                context.Note.Add(newNote);
                await context.SaveChangesAsync();
                Array.ForEach(note.Categories.ToArray(), c => {
                            try{
                                Category cat;
                                using(var transaction = context.Database.BeginTransaction()){
                                    var occurances = context.Category.Where(categoryObj => categoryObj.Name == c).ToList();
                                    if(occurances.Count() == 0) {
                                        cat = new Category {Name = c};
                                        context.Category.Add(cat);
                                        context.SaveChanges();
                                    } else {
                                        cat = occurances.FirstOrDefault();
                                    }
                                    context.NoteCategory.Add(new NoteCategory {
                                    Idnote=newNote.Idnote,
                                    Idcategory = cat.Idcategory});
                                    context.SaveChanges();
                                    transaction.Commit();
                                }
                            } catch (DbUpdateException e) {
                                Console.WriteLine(e.Message);
                            }
                        });
                await context.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int? id, [FromBody]DomainModel.Note note)
        {
            if (id == null) {
                return NotFound();
            }

            using(var context = new NTR2019ZContext()){
                var noteToUpdate = await context.Note.Include(i => i.NoteCategory).ThenInclude(noteCategories => noteCategories.IdcategoryNavigation).FirstOrDefaultAsync(note => note.Idnote == id);

                if(noteToUpdate == null){
                    return NotFound();
                }

                context.Entry(noteToUpdate).Property("Timestamp").OriginalValue = note.Timestamp;

                try
                {
                    noteToUpdate.Date = note.Date;
                    noteToUpdate.Title = note.Title;
                    noteToUpdate.Description = note.Description;
                    noteToUpdate.IsMarkdown = note.IsMarkdown;

                    foreach (string category in note.Categories) {
                        var cat = await context.Category.Where(categoryObj => categoryObj.Name == category).FirstOrDefaultAsync();

                        if(cat == null) {
                            cat = new Category {Name = category};
                            context.Category.Add(cat);
                        }
                        var noteCategory = await context.NoteCategory.Where(nc => nc.IdcategoryNavigation.Name == category && nc.Idnote == noteToUpdate.Idnote).FirstOrDefaultAsync();
                        if(noteCategory == null) {
                            context.Add(new NoteCategory { Idnote = noteToUpdate.Idnote, IdcategoryNavigation=cat});
                        }
                    }

                    await context.SaveChangesAsync();
                    return Ok();
                } catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if(databaseEntry == null)
                    {
                        return StatusCode(500, "The record you attempted to edit "
                        + "was deleted.");
                    } else {
                        noteToUpdate.Timestamp = (byte[])noteToUpdate.Timestamp;
                        return StatusCode(500, "The record you attempted to edit "
                        + "was modified by another user after you got the original value. The "
                        + "edit operation was canceled and the current values in the database "
                        + "have been displayed. If you still want to edit this record, click "
                        + "the Save button again. Otherwise click the Back to List hyperlink.");
                    }
                } catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }

                return Ok();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id == null) {
                return NotFound();
            }

            using(var context = new NTR2019ZContext()) {
                try {
                    var note = await context.Note.Where(n => n.Idnote == id).FirstOrDefaultAsync();
                    var noteCategory = await context.NoteCategory.Where(nc => nc.Idnote == id).ToListAsync();
                    Array.ForEach(noteCategory.ToArray(), nc => {
                        context.NoteCategory.Attach(nc);
                        context.NoteCategory.Remove(nc);
                    });
                    context.Note.Attach(note);
                    context.Note.Remove(note);
                    await context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                        ModelState.AddModelError("Title","Something went wrong with deleting");
                }

                return Ok();
            }
        }
    }
}
