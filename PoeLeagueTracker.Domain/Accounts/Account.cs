using PoeLeagueTracker.Domain.Characters;

namespace PoeLeagueTracker.Domain.Accounts
{
    public class Account
    {
        public string AccountName { get; private init; }
        public bool IsTwitchLinked { get; private set; }
        public string? TwitchUsername { get; private set; }
        public int CompletedChallenges { get; private set; }
        public const int MaxChallenges = 40;
        public List<Character> Characters { get; private set; }

        // Parameterless constructor for EF purposes
        public Account() { }

        private Account(string accountName, List<Character> characters, bool isTwitchLinked, string? twitchUsername, int completedChallenges)
        {
            AccountName = accountName;
            Characters = characters;
            IsTwitchLinked = isTwitchLinked;
            TwitchUsername = twitchUsername;
            CompletedChallenges = completedChallenges;
        }

        public static Account CreateAccount(string accountName, List<Character> characters, bool isTwitchLinked, string? twitchUsername, int completedChallenges)
        {
            return new Account(accountName, characters, isTwitchLinked, twitchUsername, completedChallenges);
        }

        public void UpdateWithoutCharacters(bool isTwitchLinked, string? twitchUsername, int completedChallenges)
        {
            IsTwitchLinked = isTwitchLinked;
            TwitchUsername = twitchUsername;
            CompletedChallenges = completedChallenges;
        }
    }
}
