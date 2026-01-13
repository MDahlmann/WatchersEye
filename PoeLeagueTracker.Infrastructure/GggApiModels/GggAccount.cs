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
        public GggChallenges GggChallenges { get; set; }

        [JsonPropertyName("twitch")]
        public GggTwitch GggTwitch { get; set; }

        public GggAccount(string name, bool isTwitchLinked, GggChallenges gggChallenges, GggTwitch gggTwitch)
        {
            Name = name;
            IsTwitchLinked = isTwitchLinked;
            GggChallenges = gggChallenges;
            GggTwitch = gggTwitch;
        }
    }
}