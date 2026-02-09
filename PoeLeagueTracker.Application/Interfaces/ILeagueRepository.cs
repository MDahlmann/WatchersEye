using PoeLeagueTracker.Domain.Leagues;

namespace PoeLeagueTracker.Application.Interfaces
{
    public interface ILeagueRepository
    {
        Task AddLeagueAsync(League league);
        Task<League> GetLeagueAsync(string leagueName);
        Task SaveChangesAsync();
    }
}
