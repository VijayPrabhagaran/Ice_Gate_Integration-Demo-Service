using IceGate_Demo.Entities;
using Microsoft.EntityFrameworkCore;

namespace IceGate_Demo.Dbcontext
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }
        public DbSet<IntegrationRequest> IntegrationRequest { get; set; }
        public DbSet<IntegrationResponse> IntegrationResponse { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntegrationRequest>().ToTable("DemoIntegrationRequest", "DemoIGI");
            modelBuilder.Entity<IntegrationResponse>().ToTable("DemoIntegrationResponse", "DemoIGI");
            modelBuilder.Entity<IntegrationRequest>().HasKey(e => e.IntegrationRequestId);
            modelBuilder.Entity<IntegrationRequest>().Property(e => e.AckId)
             .HasDefaultValueSql("NEWID()");
        }
    }
}
