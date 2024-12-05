using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesLiftel.Data;
using NotesLiftel.Models;
using System.Net.Mime;

namespace NotesLiftel.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext _notesDbContext;

        public NoteRepository(NotesDbContext context) 
        {
            _notesDbContext = context;
        }
        public async Task<bool> AddNote(Note note)
        {
            if(note == null)
                return false;

            note.CreatedAt = DateTime.Now;
            _notesDbContext.Add(note);
            await _notesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNoteById(int id)
        {
            var noteToDelete = await _notesDbContext.Notes.FirstOrDefaultAsync(b=> b.Id == id);
            if (noteToDelete == null) 
                return false;

            _notesDbContext.Notes.Remove(noteToDelete);
            await _notesDbContext.SaveChangesAsync();
            return true;    
        }

        public async Task<List<Note>> GetAllNotes()
        {
            return await _notesDbContext.Notes.ToListAsync();
        }

        public async Task<Note?> GetNoteById(int id)
        {
            return await _notesDbContext.Notes.FirstOrDefaultAsync(b=> b.Id == id);
        }

        public async Task<bool> UpdateNote(int id, [FromBody] Note updateNote)
        {
            var noteToUpdated = await _notesDbContext.Notes.FirstOrDefaultAsync(b => b.Id == id);
            if(noteToUpdated == null) 
                return false;

            noteToUpdated.Title = updateNote.Title;
            noteToUpdated.Content = updateNote.Content;
            noteToUpdated.UpdateAt = DateTime.Now;
            await _notesDbContext.SaveChangesAsync();
            return true;
        }
    }
}
