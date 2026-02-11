using PoeLeagueTracker.Domain.Accounts;
using PoeLeagueTracker.Domain.Leagues;

namespace PoeLeagueTracker.Domain.Characters
{
    public class Character
    {
        public string Id { get; private init; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public ClassName ClassName { get; private set; }
        public long Experience { get; private set; }
        public int Rank { get; private set; }
        public bool Dead { get; private set; }
        public bool Retired { get; private set; }
        public int? Depth { get; private set; }
        public int Challenges { get; private set; }

        // Foreign keys
        public string LeagueName { get; private set; }
        public string AccountName { get; private set; }

        // Navigation properties
        public League League { get; private set; }
        public Account Account { get; private set; }

        // Parameterless constructor for EF purposes
        public Character() { }

        private Character(
            string id, string name, int level, ClassName className, long experience,
            int rank, bool dead, bool retired, int? depth, int challenges,
            string leagueName, string accountName, League league, Account account)
        {
            Id = id;
            Name = name;
            Level = level;
            ClassName = className;
            Experience = experience;
            Rank = rank;
            Dead = dead;
            Retired = retired;
            Depth = depth;
            Challenges = challenges;
            LeagueName = leagueName;
            AccountName = accountName;
            League = league;
            Account = account;
        }

        public static Character CreateCharacter(
            string id, string name, int level, ClassName className, long experience,
            int rank, bool dead, bool retired, int? depth, int challenges,
            string leagueName, string accountName, League league, Account account)
        {
            return new Character(id, name, level, className, experience, rank, dead,
                retired, depth, challenges, leagueName, accountName, league, account);
        }

        public void Update(
            string name, int level, ClassName classname, long experience, int rank,
            bool dead, bool retired, int? depth, int challenges)
        {
            Name = name;
            Level = level;
            ClassName = classname;
            Experience = experience;
            Rank = rank;
            Dead = dead;
            Retired = retired;
            Depth = depth;
            Challenges = challenges;
        }
    }
}