using PoeLeagueTracker.Domain.Characters;

namespace PoeLeagueTracker.Domain.Accounts
{
    public class Account
    {
        public string AccountName { get; private init; }
        public List<Character> Characters { get; private set; } = [];
        public bool IsTwitchLinked { get; private set; }
        public string TwitchUsername { get; private init; }
        public int CompletedChallenges { get; private set; }
        public const int MaxChallenges = 40;

        // Parameterless constructor for EF purposes
        public Account() { }

        private Account(string accountName, bool isTwitchLinked, string twitchUsername, int completedChallenges)
        {
            AccountName = accountName;
            IsTwitchLinked = isTwitchLinked;
            TwitchUsername = twitchUsername;
            CompletedChallenges = completedChallenges;
        }

        public static Account CreateAccount(string accountName, bool isTwitchLinked, string twitchUsername, int completedChallenges)
        {
            return new Account(accountName, isTwitchLinked, twitchUsername, completedChallenges);
        }
    }
}
