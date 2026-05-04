using PoeLeagueTracker.Application.RepositoryInterfaces;
using PoeLeagueTracker.Application.ServiceInterfaces;
using PoeLeagueTracker.Domain.Users;
using System.Security.Authentication;

namespace PoeLeagueTracker.Application.Queries.LoginQuery
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, LoginResponse>
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginQueryHandler(IUserRepository userRepo, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponse?> HandleAsync(LoginQuery query)
        {
            var user = await _userRepo.GetUserByNameAsync(query.Username);

            if (user == null)
                throw new AuthenticationException("Invalid username or password.");

            if (!_passwordHasher.VerifyPassword(query.Password, user.HashedPassword))
                throw new AuthenticationException("Invalid username or password.");

            var token = _jwtTokenService.GenerateToken(user);

            return new LoginResponse(token, user.Username, user.Role);
        }
    }
}
