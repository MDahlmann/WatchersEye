using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Domain.Leagues;
using PoeLeagueTracker.Shared.DTOs;

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

        async Task<LeagueDto?> ILeagueRepository.GetLeagueDtoAsync(string leagueName)
        {
            var leagueDto = await _db.Leagues
                .Where(l => l.LeagueName == leagueName)
                .Select(l => new LeagueDto(
                    l.LeagueName,
                    l.Accounts.Select(a => new AccountDto(
                        a.AccountName,
                        a.CompletedChallenges,
                        a.Characters.Select(c => new CharacterDto(
                            c.Id,
                            c.Name,
                            c.Level,
                            c.ClassName.ToString(),
                            c.Experience,
                            c.Rank,
                            c.Dead,
                            c.Retired,
                            c.Depth
                        ))
                    ))
                )).FirstOrDefaultAsync();

            return leagueDto;
        }

        async Task<League?> ILeagueRepository.GetLeagueTrackedAsync(string leagueName)
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