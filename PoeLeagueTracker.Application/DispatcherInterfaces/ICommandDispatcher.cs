using PoeLeagueTracker.Application.Commands;

namespace PoeLeagueTracker.Application.DispatcherInterfaces
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
