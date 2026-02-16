
using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Application.Leagues.SyncLeague;

namespace PoeLeagueTracker.Api.Workers
{
    public class SyncLeagueWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private const string _leagueToUpdate = "FizzministersAndTheFlaming COCKS (PL75337)";
        //private const string _leagueToUpdate = "Secrets Of The COCK (PL71449)";

        public SyncLeagueWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Worker is a singleton and needs its own scope for services,
                // so as to not keep the services from the main scope alive for
                // the entire application runtime.
                await using (var scope = _serviceScopeFactory.CreateAsyncScope())
                {
                    var syncHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<SyncLeagueCommand>>();

                    await syncHandler.HandleAsync(new SyncLeagueCommand(_leagueToUpdate));
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
