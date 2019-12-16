using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
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
                if(startDate != null && startDate != "Invalid date") {
                    notes = notes.Where(n => n.Date >= Convert.ToDateTime(startDate)).ToList();
                }
                if(endDate != null && endDate != "Invalid date") {
                    notes = notes.Where(n => n.Date <= Convert.ToDateTime(startDate)).ToList();
                }
                var categories = await context.Category.ToListAsync();
                return Ok(new { pageOfNotes=PaginatedList<DomainModel.Note>.Create(notes.Select(note => new DomainModel.Note(note)).AsQueryable(), page ?? 1, pageSize), categories=categories.Select(c => c.Name), pager=1});
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
                return Ok(new { data=new DomainModel.Note(note)});
            }
        }
    }
}
