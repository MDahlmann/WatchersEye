using System.Text.Json.Serialization;

namespace PoeLeagueTracker.Infrastructure.ApiModels
{
    public class GggDepth
    {
        [JsonPropertyName("default")]
        public int DefaultDepth { get; set; }

        [JsonPropertyName("solo")]
        public int SoloDepth { get; set; }
    }
}
