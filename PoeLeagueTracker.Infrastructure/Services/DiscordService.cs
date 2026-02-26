using PoeLeagueTracker.Application.ServiceInterfaces;
using PoeLeagueTracker.Domain.Characters;
using PoeLeagueTracker.Infrastructure.RefitInterfaces;
using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Infrastructure.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly IDiscordApi _discordApi;

        public DiscordService(IDiscordApi discordApi)
        {
            _discordApi = discordApi;
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
                            url: $"https://www.pathofexile.com/account/view-profile/{rippedChar.AccountName.Replace("#", "%23")}/characters/{rippedChar.Name}"),
                        fields: new List<Object>(),
                        thumbnail: new DiscordThumbnail(
                            url: ""),
                        url: $"https://www.pathofexile.com/account/view-profile/{rippedChar.AccountName.Replace("#", "%23")}/characters/{rippedChar.Name}")
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

            await _discordApi.RipNotification(notification);
        }
    }
}
