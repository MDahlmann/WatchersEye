namespace PoeLeagueTracker.Shared.DTOs
{
    public record CharacterDto(
        string Id,
        string Name,
        int Level,
        string ClassName,
        long Experience,
        int Rank,
        bool Dead,
        bool Retired,
        int? Depth);
}