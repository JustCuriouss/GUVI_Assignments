using Microsoft.EntityFrameworkCore;
using PolicyNotes.Models;
using System.Collections.Generic;

namespace PolicyNotes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<PolicyNote> PolicyNotes { get; set; } = default!;
    }
}
