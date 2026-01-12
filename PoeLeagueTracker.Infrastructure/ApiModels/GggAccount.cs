using System.Text.Json.Serialization;

namespace PoeLeagueTracker.Infrastructure.ApiModels
{
    public class GggAccount
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("isTwitchLinked")]
        public bool IsTwitchLinked { get; set; }

        [JsonPropertyName("challenges")]
        public GggChallenges Challenges { get; set; }

        [JsonPropertyName("twitch")]
        public GggTwitch Twitch { get; set; }
    }
}