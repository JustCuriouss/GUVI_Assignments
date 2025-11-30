namespace PolicyNotes.DTOs
{
    public class PolicyNoteCreateDto
    {
        public string PolicyNumber { get; set; } = default!;
        public string Note { get; set; } = default!;
    }
}
