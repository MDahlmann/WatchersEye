using PoeLeagueTracker.Domain.Leagues;
using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Application.Interfaces
{
    public interface ILeagueRepository
    {
        Task AddLeagueAsync(League league);
        Task<League?> GetLeagueTrackedAsync(string leagueName);
        Task<LeagueDto?> GetLeagueDtoAsync(string leagueName);
        Task SaveChangesAsync();
    }
}
