using FernandoApexa.Domain;
using Microsoft.EntityFrameworkCore;

namespace FernandoApexa.Persistence
{
    public class FernandoApexaDbContext(DbContextOptions<FernandoApexaDbContext> options) : DbContext(options)
    {
        public DbSet<Advisor> Advisors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}