using Microsoft.AspNetCore.Mvc;
using NotesLiftel.Models;

namespace NotesLiftel.Repository
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllNotes();
        Task<Note?> GetNoteById(int id);
        Task<bool> AddNote(Note note);
        Task<bool> UpdateNote(int id, [FromBody] Note updateNote);
        Task<bool> DeleteNoteById(int id);
    }
}
