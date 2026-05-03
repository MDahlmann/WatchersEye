using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Application.RepositoryInterfaces;
using PoeLeagueTracker.Domain.Users;

namespace PoeLeagueTracker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PoeTrackerDbContext _db;

        public UserRepository(PoeTrackerDbContext db) => _db = db;

        async Task IUserRepository.AddUserAsync(User user)
        {
            await _db.AddAsync(user);
        }

        async Task<bool> IUserRepository.UsernameExistsAsync(string username)
        {
            return await _db.Users
                .AnyAsync(u => u.Username == username);
        }

        async Task IUserRepository.SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
