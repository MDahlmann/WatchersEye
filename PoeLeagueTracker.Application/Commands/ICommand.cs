namespace PoeLeagueTracker.Application.Commands
{
    public interface ICommand;

    public interface ICommand<out TResponse>;
}
