using Microsoft.EntityFrameworkCore;
using PoeLeagueTracker.Application;
using PoeLeagueTracker.Infrastructure;
using Refit;

namespace PoeLeagueTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
                .AddRefitClient<IGggApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://api.pathofexile.com");
                    c.DefaultRequestHeaders.Add("User-Agent", "OAuth PoeLeagueTracker/1.0 (contact: dahlmann.mikkel@gmail.com)");
                });

            builder.Services.AddDbContext<PoeTrackerDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PoeLeagueTrackerDb"))
            );

            builder.Services.AddScoped<IPoeLadderService, PoeLadderService>();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.MapGet("/ladder/{leagueId}", async (IPoeLadderService ladderService, string leagueId) =>
            {
                var accounts = await ladderService.GetLadderDataAsync(leagueId);
                return Results.Ok(accounts);
            });

            app.Run();
        }
    }
}