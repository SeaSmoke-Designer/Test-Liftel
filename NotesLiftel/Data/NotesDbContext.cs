using Microsoft.EntityFrameworkCore;
using NotesLiftel.Models;

namespace NotesLiftel.Data
{
    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }
    }
}
