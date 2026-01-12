namespace PoeLeagueTracker.Domain.Characters
{
    public class Character
    {
        public string Id { get; private init; }
        public string Name { get; private init; }
        public int Level { get; private set; }
        public ClassName ClassName { get; private set; }
        public long Experience { get; private set; }
        public int Rank { get; private set; }
        public bool Dead { get; private set; }
        public bool Retired { get; private set; }
        public bool IsPublic { get; private set; }
        public int Depth { get; private set; }

        // Parameterless constructor for EF purposes
        public Character() { }

        private Character(string id, string name, int level, ClassName className, long experience, int rank, bool dead, bool retired, bool isPublic, int depth)
        {
            Id = id;
            Name = name;
            Level = level;
            ClassName = className;
            Experience = experience;
            Rank = rank;
            Dead = dead;
            Retired = retired;
            IsPublic = isPublic;
            Depth = depth;
        }

        public static Character CreateCharacter(string id, string name, int level, ClassName className, long experience, int rank, bool dead, bool retired, bool isPublic, int depth)
        {
            return new Character(id, name, level, className, experience, rank, dead, retired, isPublic, depth);
        }
    }
}
