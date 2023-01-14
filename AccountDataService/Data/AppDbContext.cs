using AccountDataService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountDataService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt ) : base(opt)
        {
            
        }

        public DbSet<Passport> Passports { get; set; }
        public DbSet<AccountData> AccountData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Passport>()
                .HasMany(p => p.AccountData)
                .WithOne(p=> p.Passport!)
                .HasForeignKey(p => p.PassportId);

            modelBuilder
                .Entity<AccountData>()
                .HasOne(p => p.Passport)
                .WithMany(p => p.AccountData)
                .HasForeignKey(p =>p.PassportId);
        }
    }
}