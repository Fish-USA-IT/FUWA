using System.Text.Json.Serialization;

namespace FishUsaWebApp.Models.SuiteQlRows;

public sealed class CustomerSearchSuiteQlRow
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("entityid")]
    public string? EntityId { get; set; }

    [JsonPropertyName("companyname")]
    public string? CompanyName { get; set; }

    [JsonPropertyName("firstname")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastname")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("isinactive")]
    public string? IsInactive { get; set; }

    public string DisplayName =>
        !string.IsNullOrWhiteSpace(CompanyName)
            ? CompanyName
            : $"{FirstName} {LastName}".Trim();
}