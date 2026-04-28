using PoeLeagueTracker.Application.Queries;

namespace PoeLeagueTracker.Application.DispatcherInterfaces
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
