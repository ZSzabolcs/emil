using emil.Models;
using Microsoft.EntityFrameworkCore;

namespace emil.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SentEmail> SentEmails { get; set; }
        public DbSet<User> Users { get; set; }
    }
}