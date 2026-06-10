using System.Text.Json.Serialization;

namespace FishUsaWebApp.Models.SuiteQlRows;

public sealed class EmployeeImageSuiteQlRow
{
    [JsonPropertyName("employee_id")]
    public string? EmployeeId { get; set; }

    [JsonPropertyName("employee_name")]
    public string? EmployeeName { get; set; }

    [JsonPropertyName("image_file_id")]
    public string? ImageFileId { get; set; }

    [JsonPropertyName("file_name")]
    public string? FileName { get; set; }

    [JsonPropertyName("file_type")]
    public string? FileType { get; set; }

    [JsonPropertyName("file_url")]
    public string? FileUrl { get; set; }
}