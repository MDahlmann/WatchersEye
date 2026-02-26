using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Application.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountsByNameAsync(List<string> accountNames);
    }
}
