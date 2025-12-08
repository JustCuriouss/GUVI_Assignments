namespace TicketsApi
{
    public class Ticket
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Severity { get; set; }
        public DateTime TargetDate { get; set; }
        public string Status { get; set; }

    }
}
