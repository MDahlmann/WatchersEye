using PoeLeagueTracker.Shared.Enums;

namespace PoeLeagueTracker.Application.Queries.LoginQuery
{
    public record LoginQuery(string Username, string Password) : IQuery<LoginResponse>;

    public record LoginResponse(string Token, string Username, UserRole Role);
}
