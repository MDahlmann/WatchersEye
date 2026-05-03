using PoeLeagueTracker.Shared.Enums;

namespace PoeLeagueTracker.Application.Commands.CreateUserCommand
{
    public record CreateUserCommand(string Username, string Password, UserRole Role) : ICommand<Guid>
    {
    }
}
