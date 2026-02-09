using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Application.Interfaces
{
    public interface ILadderRepository
    {
        Task AddAccountsAsync(IEnumerable<Account> accounts);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task SaveChangesAsync();
    }
}
