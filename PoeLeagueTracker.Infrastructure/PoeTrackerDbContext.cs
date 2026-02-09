using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Domain.Accounts;
using PoeLeagueTracker.Domain.Characters;
using PoeLeagueTracker.Domain.Leagues;

namespace PoeLeagueTracker.Infrastructure
{
    public class PoeTrackerDbContext(DbContextOptions<PoeTrackerDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<League> Leagues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountName);

            modelBuilder.Entity<Character>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<League>()
                .HasKey(l => l.LeagueName);
        }
    }
}