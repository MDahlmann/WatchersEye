using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Api.Workers;
using PoeLeagueTracker.Application.Commands;
using PoeLeagueTracker.Application.Commands.SyncLeagueCommand;
using PoeLeagueTracker.Application.DispatcherInterfaces;
using PoeLeagueTracker.Application.Queries;
using PoeLeagueTracker.Application.Queries.GetActiveLeagueQuery;
using PoeLeagueTracker.Application.Queries.GetLeagueNamesQuery;
using PoeLeagueTracker.Application.Queries.GetLeagueQuery;
using PoeLeagueTracker.Application.RepositoryInterfaces;
using PoeLeagueTracker.Application.ServiceInterfaces;
using PoeLeagueTracker.Domain.Users;
using PoeLeagueTracker.Infrastructure;
using PoeLeagueTracker.Infrastructure.Dispatchers;
using PoeLeagueTracker.Infrastructure.RefitInterfaces;
using PoeLeagueTracker.Infrastructure.Repositories;
using PoeLeagueTracker.Infrastructure.Services;
using PoeLeagueTracker.Shared.DTOs;
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

            var useMockData = builder.Configuration.GetValue<bool>("UseMockData");

            // Add services to the container.
            if (builder.Environment.IsDevelopment() || useMockData)
            {
                builder.Services.AddScoped<IGggApi, GggApiMock>();
            }
            else
            {
                builder.Services
                    .AddRefitClient<IGggApi>()
                    .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri("https://api.pathofexile.com");
                        c.DefaultRequestHeaders.Add("User-Agent", "OAuth PoeLeagueTracker/1.0 (contact: dahlmann.mikkel@gmail.com)");
                    })
                    .AddStandardResilienceHandler();
            }

            builder.Services
                .AddRefitClient<IDiscordApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://discord.com/api/webhooks");
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
            builder.Services.AddScoped<IDiscordService, DiscordService>();
            builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICommandHandler<SyncLeagueCommand>, SyncLeagueCommandHandler>();
            builder.Services.AddScoped<IQueryHandler<GetLeagueQuery, LeagueDto?>, GetLeagueQueryHandler>();
            builder.Services.AddScoped<IQueryHandler<GetLeagueNamesQuery, List<string>?>, GetLeagueNamesQueryHandler>();
            builder.Services.AddScoped<IQueryHandler<GetActiveLeagueQuery, string?>, GetActiveLeagueQueryHandler>();
            builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<UserFactory>();


            builder.Services.AddHostedService<SyncLeagueWorker>();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            // Only needed because of API-controller 'proof-of'
            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            var originApiUrl = builder.Configuration["OriginApiUrl"];
            builder.Services.AddCors(options => options.AddPolicy("CustomPolicy", builder =>
            {
                builder.WithOrigins(originApiUrl!);
                builder.WithMethods("GET");
                builder.WithHeaders("Content-Type");
            }));

            var app = builder.Build();

            // Auto-migrates new migrations on startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PoeTrackerDbContext>();
                dbContext.Database.Migrate();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors("CustomPolicy");
            app.UsePathBase("/api");

            // Only needed because of API-controller 'proof-of'
            app.MapControllers();

            // Minimal API implementation
            app.MapGet("league/{leagueId}", async (IQueryDispatcher queryDispatcher, string leagueId) =>
            {
                var league = await queryDispatcher.DispatchAsync<GetLeagueQuery, LeagueDto?>(new GetLeagueQuery(leagueId));

                if (league is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(league);
            });

            app.MapGet("allLeagueNames", async (IQueryDispatcher queryDispatcher) =>
            {
                var leagues = await queryDispatcher.DispatchAsync<GetLeagueNamesQuery, List<string>?>(new GetLeagueNamesQuery());

                if (leagues is null) { return Results.NotFound(); }

                return Results.Ok(leagues);
            });

            app.MapGet("activeLeague", async (IQueryDispatcher queryDispatcher) =>
            {
                var activeLeague = await queryDispatcher.DispatchAsync<GetActiveLeagueQuery, string?>(new GetActiveLeagueQuery());

                if (activeLeague is null) { return Results.NotFound(); }

                return Results.Ok(activeLeague);
            });

            app.Run();
        }
    }
}