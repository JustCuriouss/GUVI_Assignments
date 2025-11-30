using System.ComponentModel.DataAnnotations;

namespace PolicyNotes.Models
{
    public class PolicyNote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PolicyNumber { get; set; } = default!;
        [Required]
        public string Note { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
