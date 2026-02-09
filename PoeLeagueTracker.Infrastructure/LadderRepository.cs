using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Application;
using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Infrastructure
{
    public class LadderRepository : ILadderRepository
    {
        private readonly PoeTrackerDbContext _db;

        public LadderRepository(PoeTrackerDbContext db) => _db = db;

        async Task ILadderRepository.AddAccountsAsync(IEnumerable<Account> accounts)
        {
            await _db.Accounts.AddRangeAsync(accounts);
        }

        async Task<IEnumerable<Account>> ILadderRepository.GetAllAccountsAsync()
        {
            List<Account> accounts = [];

            accounts = await _db.Accounts
                .Include(account => account.Characters)
                .ToListAsync();

            return accounts;
        }

        async Task ILadderRepository.SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
