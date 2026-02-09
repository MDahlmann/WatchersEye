using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Domain.Leagues;

namespace PoeLeagueTracker.Infrastructure
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly PoeTrackerDbContext _db;

        public LeagueRepository(PoeTrackerDbContext db) => _db = db;

        async Task ILeagueRepository.AddLeagueAsync(League league)
        {
            await _db.Leagues.AddAsync(league);
        }

        async Task<League?> ILeagueRepository.GetLeagueAsync(string leagueName)
        {
            var league = await _db.Leagues
                    .AsSplitQuery()
                    .Where(l => l.LeagueName == leagueName)
                    .Include(l => l.Accounts)
                    .ThenInclude(a => a.Characters)
                    .SingleOrDefaultAsync();

            return league;
        }

        async Task ILeagueRepository.SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}