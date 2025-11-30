using PolicyNotes.Models;
using PolicyNotes.Repositories;

namespace PolicyNotes.Services
{
    public class PolicyNotesService : IPolicyNotesService
    {
        private readonly IPolicyNoteRepository _repo;
        public PolicyNotesService(IPolicyNoteRepository repo) => _repo = repo;

        public async Task<PolicyNote> AddNoteAsync(string policyNumber, string note)
        {
            var entity = new PolicyNote
            {
                PolicyNumber = policyNumber,
                Note = note,
                CreatedAt = DateTime.UtcNow
            };
            return await _repo.AddAsync(entity);
        }

        public async Task<IEnumerable<PolicyNote>> GetAllNotesAsync() => await _repo.GetAllAsync();

        public async Task<PolicyNote?> GetNoteByIdAsync(int id) => await _repo.GetByIdAsync(id);
    }
}
