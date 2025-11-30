using Microsoft.EntityFrameworkCore;
using PolicyNotes.Data;
using PolicyNotes.Models;

namespace PolicyNotes.Repositories
{
    public class PolicyNoteRepository : IPolicyNoteRepository
    {
        private readonly AppDbContext _db;
        public PolicyNoteRepository(AppDbContext db) => _db = db;

        public async Task<PolicyNote> AddAsync(PolicyNote note)
        {
            _db.PolicyNotes.Add(note);
            await _db.SaveChangesAsync();
            return note;
        }

        public async Task<IEnumerable<PolicyNote>> GetAllAsync()
        {
            return await _db.PolicyNotes.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

        public async Task<PolicyNote?> GetByIdAsync(int id)
        {
            return await _db.PolicyNotes.FindAsync(id);
        }
    }
}
