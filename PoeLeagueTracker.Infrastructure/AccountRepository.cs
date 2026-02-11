using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PoeTrackerDbContext _db;

        public AccountRepository(PoeTrackerDbContext db) => _db = db;

        async Task<List<Account>> IAccountRepository.GetAccountsByNameAsync(List<string> accountNames)
        {
            var accounts = await _db.Accounts
                .Where(a => accountNames.Contains(a.AccountName))
                .ToListAsync();

            return accounts;
        }
    }
}
