using System.Text.Json.Serialization;

namespace PoeLeagueTracker.Infrastructure.ApiModels
{
    public class GggTwitch
    {
        [JsonPropertyName("twitch")]
        public string TwitchUsername { get; set; }

        public GggTwitch(string twitchUsername)
        {
            TwitchUsername = twitchUsername;
        }
    }
}
