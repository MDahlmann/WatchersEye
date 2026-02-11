using PoeLeagueTracker.Domain.Characters;

namespace PoeLeagueTracker.Domain.Accounts
{
    public class Account
    {
        public string AccountName { get; private init; }
        public List<Character> Characters { get; private init; } = [];

        // Parameterless constructor for EF purposes
        public Account() { }

        private Account(string accountName)
        {
            AccountName = accountName;
        }

        public static Account CreateAccount(string accountName)
        {
            return new Account(accountName);
        }
    }
}