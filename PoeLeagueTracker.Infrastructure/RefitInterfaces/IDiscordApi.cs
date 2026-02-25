using PoeLeagueTracker.Shared.DTOs;
using Refit;

namespace PoeLeagueTracker.Infrastructure.RefitInterfaces
{
    public interface IDiscordApi
    {
        [Headers("Content-Type: application/json;")]
        [Post("/1476302257090990263/ipuG56pRJBdQAvqbhcVZs5cgx9rRh6NjCDlRrekslyT2DKWvla_vWfs1WYtaSgf__FF4")]
        Task RipNotification([Body] DiscordNotificationDto notification);
    }
}
