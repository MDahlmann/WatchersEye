using PoeLeagueTracker.Domain.Characters;

namespace PoeLeagueTracker.Application.ServiceInterfaces
{
    public interface IDiscordService
    {
        Task AnnounceRip(Character rippedChar);
    }
}
