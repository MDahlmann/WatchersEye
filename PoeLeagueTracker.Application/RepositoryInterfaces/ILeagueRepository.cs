using PoeLeagueTracker.Domain.Leagues;
using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Application.RepositoryInterfaces
{
    public interface ILeagueRepository
    {
        Task AddLeagueAsync(League league);
        Task<League?> GetLeagueTrackedAsync(string leagueName);
        Task<LeagueDto?> GetLeagueDtoAsync(string leagueName);
        Task<List<string>?> GetLeagueNamesAsync();
        Task SaveChangesAsync();
    }
}
