using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesLiftel.Models;
using NotesLiftel.Repository;

namespace NotesLiftel.Controllers
{
    [Route("notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAll()
        {
            return Ok(await noteRepository.GetAllNotes());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            if (note == null)
                return BadRequest("La nota no puede ser nula");

            var added = await noteRepository.AddNote(note);
            if (!added)
                return Conflict("Ya existe una nota con este titulo");

            return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var note = await noteRepository.GetNoteById(id);
            if (note == null) return NotFound();

            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deleted = await noteRepository.DeleteNoteById(id);
            if (!deleted) return NotFound("No se encontró la nota");

            return Ok($"Nota eliminada con ID: {id}");

        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, Note note)
        {
            note.Id = id;
            var updated = await noteRepository.UpdateNote(id,note);
            if (!updated) return BadRequest("Error al actualizar la nota");

            return Ok("Nota actualizada");
        }
    }
}
