using System.Text.Json.Serialization;

public sealed class EmployeeLoginSuiteQlRow
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("entityid")]
    public string? EntityId { get; set; }

    [JsonPropertyName("firstname")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastname")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}