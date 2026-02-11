namespace PoeLeagueTracker.Shared.DTOs
{
    public record AccountDto(
        string AccountName,
        IEnumerable<CharacterDto> Characters);
}