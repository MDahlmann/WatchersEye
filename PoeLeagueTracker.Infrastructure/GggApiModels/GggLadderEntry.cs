using System.Text.Json.Serialization;

namespace PoeLeagueTracker.Infrastructure.ApiModels
{
    public class GggLadderEntry
    {
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("dead")]
        public bool Dead { get; set; }

        [JsonPropertyName("retired")]
        public bool Retired { get; set; }

        [JsonPropertyName("public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("character")]
        public GggCharacter GggCharacter { get; set; }

        [JsonPropertyName("account")]
        public GggAccount GggAccount { get; set; }

        public GggLadderEntry(int rank, bool dead, bool retired, bool isPublic, GggCharacter gggCharacter, GggAccount gggAccount)
        {
            Rank = rank;
            Dead = dead;
            Retired = retired;
            IsPublic = isPublic;
            GggCharacter = gggCharacter;
            GggAccount = gggAccount;
        }
    }
}
