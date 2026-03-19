namespace PoeLeagueTracker.Domain.Characters
{
    public enum ClassName
    {
        None,
        Marauder,
        Juggernaut,
        Berserker,
        Chieftain,
        Duelist,
        Slayer,
        Gladiator,
        Champion,
        Ranger,
        Deadeye,
        Raider,
        Pathfinder,
        Shadow,
        Assassin,
        Saboteur,
        Trickster,
        Witch,
        Necromancer,
        Occultist,
        Elementalist,
        Templar,
        Inquisitor,
        Hierophant,
        Guardian,
        Scion,
        Ascendant,
        Reliquarian
    }

    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value, T defaultValue = default) where T : struct, Enum
        {
            if (Enum.TryParse<T>(value, true, out var result))
            {
                return result;
            }
            return defaultValue;
        }
    }
}