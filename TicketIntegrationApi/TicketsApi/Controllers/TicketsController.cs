using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTickets()
        {
            List<Ticket> tickets = new List<Ticket>
            {
                new Ticket()
                {
                    Id = 1,
                    ShortDescription = "Error",
                    Description = "Me not found",
                    CreatedDate = new DateTime(2025,12,8),
                    Severity = "Extreme",
                    TargetDate = new DateTime(2025,12,10),
                    Status = "Open"
                },
                new Ticket()
                {
                    Id = 2,
                    ShortDescription = "Error",
                    Description = "You not found",
                    CreatedDate = new DateTime(2025,12,8),
                    Severity = "Urgent",
                    TargetDate = new DateTime(2025,12,20),
                    Status = "Open"
                }
            };
            return Ok(tickets);
        }
    }
}
