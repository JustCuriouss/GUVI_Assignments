using PolicyNotes.Models;

namespace PolicyNotes.Repositories
{
    public interface IPolicyNoteRepository
    {
        Task<PolicyNote> AddAsync(PolicyNote note);
        Task<IEnumerable<PolicyNote>> GetAllAsync();
        Task<PolicyNote?> GetByIdAsync(int id);
    }
}
