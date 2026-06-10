using System.Text.Json.Serialization;

namespace FishUsaWebApp.Models.SuiteQlRows;

public sealed class SuiteQlResponse<TItem>
{
    [JsonPropertyName("links")]
    public List<SuiteQlLink> Links { get; set; } = [];

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    [JsonPropertyName("items")]
    public List<TItem> Items { get; set; } = [];

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("totalResults")]
    public int TotalResults { get; set; }
}

public sealed class SuiteQlLink
{
    [JsonPropertyName("rel")]
    public string? Rel { get; set; }

    [JsonPropertyName("href")]
    public string? Href { get; set; }
}