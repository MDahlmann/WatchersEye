using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Domain.Accounts;
using PoeLeagueTracker.Domain.Characters;

namespace PoeLeagueTracker.Infrastructure
{
    public class PoeTrackerDbContext(DbContextOptions<PoeTrackerDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }
    }
}
