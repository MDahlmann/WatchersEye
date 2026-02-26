using System.Text.Json.Serialization;

namespace PoeLeagueTracker.Shared.DTOs
{
    public record DiscordWebhookPayload(
    [property: JsonPropertyName("content")] string? content,
    [property: JsonPropertyName("embeds")] List<DiscordEmbed>? embeds,
    [property: JsonPropertyName("components")] List<Object>? components);

    public record DiscordEmbed(
        [property: JsonPropertyName("title")] string title,
        [property: JsonPropertyName("color")] int color,
        [property: JsonPropertyName("author")] DiscordAuthor author,
        [property: JsonPropertyName("fields")] List<object> fields, // Use a specific record if you need fields later
        [property: JsonPropertyName("thumbnail")] DiscordThumbnail thumbnail,
        [property: JsonPropertyName("url")] string url);

    public record DiscordAuthor(
        [property: JsonPropertyName("name")] string name,
        [property: JsonPropertyName("url")] string url);

    public record DiscordThumbnail(
        [property: JsonPropertyName("url")] string url);
}
