using PoeLeagueTracker.Domain.Characters;

namespace PoeLeagueTracker.Domain.Leagues
{
    public class League
    {
        public string LeagueName { get; private set; }
        public List<Character> Characters { get; private init; } = [];

        // Parameterless constructor for EF purposes
        public League() { }

        private League(string leagueName)
        {
            LeagueName = leagueName;
        }

        public static League CreateLeague(string leagueName)
        {
            return new League(leagueName);
        }
    }
}
