using Microsoft.Extensions.Configuration;
using PoeLeagueTracker.Application.ServiceInterfaces;
using PoeLeagueTracker.Domain.Characters;
using PoeLeagueTracker.Infrastructure.RefitInterfaces;
using PoeLeagueTracker.Shared.DTOs;

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
            var notification = new DiscordWebhookPayload(
                content: null,
                embeds: new List<DiscordEmbed>
                {
                    new DiscordEmbed(
                        title: $"{rippedChar.Name} (lvl {rippedChar.Level}) died at rank {rippedChar.Rank}",
                        color: 16711763,
                        author: new DiscordAuthor(
                            name: rippedChar.AccountName,
                            url: $"https://www.pathofexile.com/account/view-profile/{rippedChar.AccountName.Replace("#", "%23")}/characters?characterName={rippedChar.Name}"),
                        fields: new List<Object>(),
                        thumbnail: new DiscordThumbnail(
                            url: ""),
                        url: $"https://www.pathofexile.com/account/view-profile/{rippedChar.AccountName.Replace("#", "%23")}/characters?characterName={rippedChar.Name}")
                },
                components: new List<Object>());

            //var payload = new
            //{
            //    embeds = new[] {
            //        new {
            //            title = $"{rippedChar.Name} (lvl {rippedChar.Level}) died at rank {rippedChar.Rank}",
            //            color = 16711763,
            //            author = new {
            //                name = rippedChar.AccountName,
            //                url = $"https://www.pathofexile.com/account/view-profile/{rippedChar.AccountName.Replace("#", "%23")}"
            //            },
            //            fields = Array.Empty<object>(),
            //            thumbnail = new { url = "" },
            //            url = "..."
            //        }
            //    },
            //    components = Array.Empty<object>()
            //};

            //var notification = JsonSerializer.Serialize(payload);

            var path = _config["DiscordWebhook"];

            await _discordApi.RipNotification(path, notification);
        }
    }
}
