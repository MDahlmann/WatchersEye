using PoeLeagueTracker.Domain.Users;

namespace PoeLeagueTracker.Application.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<bool> UsernameExistsAsync(string username);
        Task SaveChangesAsync();
    }
}
