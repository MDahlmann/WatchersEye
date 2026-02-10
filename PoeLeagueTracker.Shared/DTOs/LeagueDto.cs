namespace PoeLeagueTracker.Shared.DTOs
{
    public record LeagueDto(
        string LeagueName,
        IEnumerable<AccountDto> Accounts);
}
