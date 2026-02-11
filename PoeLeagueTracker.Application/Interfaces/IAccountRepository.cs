using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountsByNameAsync(List<string> accountNames);
    }
}
