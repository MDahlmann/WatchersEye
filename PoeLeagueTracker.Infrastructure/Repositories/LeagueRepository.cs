using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Application.RepositoryInterfaces;
using PoeLeagueTracker.Domain.Leagues;
using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Infrastructure.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly PoeTrackerDbContext _db;

        public LeagueRepository(PoeTrackerDbContext db) => _db = db;

        async Task ILeagueRepository.AddLeagueAsync(League league)
        {
            await _db.Leagues.AddAsync(league);
        }

        async Task<LeagueDto?> ILeagueRepository.GetLeagueDtoAsync(string leagueName)
        {
            var leagueDto = await _db.Leagues
                .Where(league => league.LeagueName == leagueName)
                .Select(league => new LeagueDto(
                    league.LeagueName,
                    league.Characters
                        .OrderBy(c => c.Rank)
                        .Select(c => new CharacterDto(
                            c.Id,
                            c.Name,
                            c.Level,
                            c.ClassName.ToString(),
                            c.Experience,
                            c.Rank,
                            c.Dead,
                            c.Retired,
                            c.Depth,
                            c.Challenges,
                            c.LeagueName,
                            c.AccountName
                    )))).FirstOrDefaultAsync();

            return leagueDto;
        }

        async Task<League?> ILeagueRepository.GetLeagueTrackedAsync(string leagueName)
        {
            var league = await _db.Leagues
                    .AsSplitQuery()
                    .Where(l => l.LeagueName == leagueName)
                    .Include(l => l.Characters)
                    .ThenInclude(c => c.Account)
                    .SingleOrDefaultAsync();

            return league;
        }

        async Task<List<string>?> ILeagueRepository.GetLeagueNamesAsync()
        {
            var leagues = await _db.Leagues
                .AsNoTracking()
                .Select(league => league.LeagueName)
                .ToListAsync();

            return leagues;
        }

        async Task ILeagueRepository.SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}