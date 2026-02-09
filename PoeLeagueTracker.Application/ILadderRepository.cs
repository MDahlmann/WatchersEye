using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Application
{
    public interface ILadderRepository
    {
        Task AddAccountsAsync(IEnumerable<Account> accounts);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task SaveChangesAsync();
    }
}
