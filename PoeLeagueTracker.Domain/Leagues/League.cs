using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Domain.Leagues
{
    public class League
    {
        public string LeagueName { get; private set; }
        public List<Account> Accounts { get; private set; }

        // Parameterless constructor for EF purposes
        public League() { }

        private League(string leagueName, List<Account> accounts)
        {
            LeagueName = leagueName;
            Accounts = accounts;
        }

        public static League CreateLeague(string leagueName, List<Account> accounts)
        {
            return new League(leagueName, accounts);
        }
    }
}
