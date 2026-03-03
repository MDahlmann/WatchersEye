
using PoeLeagueTracker.Application.Commands;
using PoeLeagueTracker.Application.Commands.SyncLeagueCommand;

namespace PoeLeagueTracker.Api.Workers
{
    public class SyncLeagueWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        //private const string _leagueToUpdate = "FizzministersAndTheFlaming COCKS (PL75337)";
        private readonly IConfiguration _config;

        public SyncLeagueWorker(IServiceScopeFactory serviceScopeFactory, IConfiguration config)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _config = config;
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

                    await syncHandler.HandleAsync(new SyncLeagueCommand(_config["ActiveLeague"]!));
                }

                var interval = Convert.ToInt32(_config["SyncInterval"]);

                await Task.Delay(interval, stoppingToken);
            }
        }
    }
}
