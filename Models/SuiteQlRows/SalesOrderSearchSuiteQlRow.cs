using System.Text.Json.Serialization;

namespace FishUsaWebApp.Models.SuiteQlRows;

public sealed class SalesOrderSearchSuiteQlRow
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("tranid")]
    public string? TranId { get; set; }

    [JsonPropertyName("trandate")]
    public string? TranDate { get; set; }

    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    [JsonPropertyName("customer_name")]
    public string? CustomerName { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("total")]
    public string? Total { get; set; }
}