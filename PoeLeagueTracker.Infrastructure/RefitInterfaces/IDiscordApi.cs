using Refit;

namespace PoeLeagueTracker.Infrastructure.RefitInterfaces
{
    public interface IDiscordApi
    {
        [Headers("Content-Type: application/json")]
        [Post("/{webhookPath}")]
        Task RipNotification([AliasAs("webhookPath")] string path, [Body] object notification);
    }
}
