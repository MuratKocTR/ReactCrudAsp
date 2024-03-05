using Microsoft.EntityFrameworkCore;

namespace ReactCrudAsp.Models
{
    public class MeetDbContext : DbContext
    {
        public MeetDbContext(DbContextOptions<MeetDbContext> options) : base(options)
        {
        }

        public DbSet<Meet> Meet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-R7L0G1K\\SQLEXPRESS;Database=ReactAsp;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;Integrated Security=true");
        }
    }

   
        
    
}
