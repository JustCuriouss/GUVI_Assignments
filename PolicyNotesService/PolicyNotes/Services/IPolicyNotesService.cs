using PolicyNotes.Models;

namespace PolicyNotes.Services
{
    public interface IPolicyNotesService
    {
        Task<PolicyNote> AddNoteAsync(string policyNumber, string note);
        Task<IEnumerable<PolicyNote>> GetAllNotesAsync();
        Task<PolicyNote?> GetNoteByIdAsync(int id);
    }
}
