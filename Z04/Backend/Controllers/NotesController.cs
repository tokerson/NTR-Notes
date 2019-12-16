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
                    notes = notes.Where(n => n.NoteCategory.Any(nc => nc.IdcategoryNavigation.Name.Equals(category))).ToList();
                }
                if(startDate != null && startDate != "Invalid date" && startDate != "null") {
                    notes = notes.Where(n => n.Date >= Convert.ToDateTime(startDate)).ToList();
                }
                if(endDate != null && endDate != "Invalid date" && endDate != "null") {
                    notes = notes.Where(n => n.Date <= Convert.ToDateTime(startDate)).ToList();
                }
                var categories = await context.Category.ToListAsync();
                var pageOfNotes = PaginatedList<DomainModel.Note>.Create(notes.Select(note => DomainModel.Note.Create(note)).AsQueryable(), page ?? 1, pageSize);
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
