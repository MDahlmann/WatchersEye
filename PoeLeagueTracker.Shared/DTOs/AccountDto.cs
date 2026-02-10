namespace PoeLeagueTracker.Shared.DTOs
{
    public record AccountDto(
        string AccountName,
        int CompletedChallenges,
        IEnumerable<CharacterDto> Characters);
}