using CQRSMediatrStudent.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CQRSMediatrStudent.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Student> Students => Set<Student>();
    }

}
