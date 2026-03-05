using Microsoft.Extensions.Configuration;
using PoeLeagueTracker.Application.ServiceInterfaces;
using PoeLeagueTracker.Domain.Characters;
using PoeLeagueTracker.Infrastructure.RefitInterfaces;

namespace PoeLeagueTracker.Infrastructure.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly IDiscordApi _discordApi;
        private readonly IConfiguration _config;

        public DiscordService(IDiscordApi discordApi, IConfiguration config)
        {
            _discordApi = discordApi;
            _config = config;
        }

        async Task IDiscordService.AnnounceRip(Character rippedChar)
        {
            var notification = new
            {
                embeds = new[] {
        new {
        author = new {
            name = string.IsNullOrWhiteSpace(rippedChar.LeagueName) ? "Unknown League" : rippedChar.LeagueName,
            url = $"https://poe.dahlmann.dev/{rippedChar.LeagueName.Replace(" ", "%20")}/"},
        title = $"{rippedChar.Name} (lvl {rippedChar.Level}) died at rank {rippedChar.Rank}",
        color = 16711682,
        description = rippedChar.AccountName ?? "Unknown Account",
        url = $"https://www.pathofexile.com/account/view-profile/{rippedChar.AccountName!.Replace("#", "%23")}/characters?characterName={rippedChar.Name}"
        }}
            };

            var path = _config["DiscordWebhook"];

            await _discordApi.RipNotification(path, notification);

        }
    }
}
