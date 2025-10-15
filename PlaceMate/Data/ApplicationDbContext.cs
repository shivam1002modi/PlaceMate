using Microsoft.EntityFrameworkCore;
using PlaceMate.Models;


namespace PlaceMate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<InterviewExperience> InterviewExperiences { get; set; }
    }
}