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
            var notification = new DiscordNotificationDto($"{rippedChar.Name} (lvl {rippedChar.Level}) just died at rank {rippedChar.Rank}");

            await _discordApi.RipNotification(notification);
        }
    }
}
