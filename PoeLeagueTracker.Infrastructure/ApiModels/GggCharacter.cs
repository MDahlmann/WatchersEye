using System.Text.Json.Serialization;

namespace PoeLeagueTracker.Infrastructure.ApiModels
{
    public class GggCharacter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("class")]
        public string ClassName { get; set; }

        [JsonPropertyName("experience")]
        public long Experience { get; set; }

        [JsonPropertyName("depth")]
        public GggDepth GggDepth { get; set; }

        public GggCharacter(string id, string name, int level, string className, long experience, GggDepth gggDepth)
        {
            Id = id;
            Name = name;
            Level = level;
            ClassName = className;
            Experience = experience;
            GggDepth = gggDepth;
        }
    }
}
