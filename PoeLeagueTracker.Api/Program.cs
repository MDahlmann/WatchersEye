using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Application.Leagues.SyncLeague;
using PoeLeagueTracker.Infrastructure;
using Refit;

namespace PoeLeagueTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DotNetEnv-libary allows .NET to read the local .env file
            DotNetEnv.Env.TraversePath().Load();

            // Add services to the container.
            builder.Services
                .AddRefitClient<IGggApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://api.pathofexile.com");
                    c.DefaultRequestHeaders.Add("User-Agent", "OAuth PoeLeagueTracker/1.0 (contact: dahlmann.mikkel@gmail.com)");
                });

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPass = Environment.GetEnvironmentVariable("DB_PASS");

            var connectionString =
                $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass}";

            builder.Services.AddDbContext<PoeTrackerDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            builder.Services.AddScoped<IPoeLadderService, PoeLadderService>();
            builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
            builder.Services.AddScoped<ICommandHandler<SyncLeagueCommand>, SyncLeagueHandler>();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.MapGet("/ladder/{leagueId}", async (IPoeLadderService ladderService,
                                                    string leagueId,
                                                    ICommandHandler<SyncLeagueCommand> upsertLeagueHandler) =>
            {
                await upsertLeagueHandler.HandleAsync(new SyncLeagueCommand(leagueId));
                var league = await ladderService.GetLeagueAsync(leagueId);
                return Results.Ok(league);
            });

            app.Run();
        }
    }
}