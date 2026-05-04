using PoeLeagueTracker.Domain.Users;

namespace PoeLeagueTracker.Application.ServiceInterfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
