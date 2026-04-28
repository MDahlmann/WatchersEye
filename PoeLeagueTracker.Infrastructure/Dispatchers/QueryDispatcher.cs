using Microsoft.Extensions.DependencyInjection;
using PoeLeagueTracker.Application.DispatcherInterfaces;
using PoeLeagueTracker.Application.Queries;

namespace PoeLeagueTracker.Infrastructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        async Task<TResult> IQueryDispatcher.DispatchAsync<TQuery, TResult>(TQuery query)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

            var result = await handler.HandleAsync(query);

            return result!;
        }
    }
}
