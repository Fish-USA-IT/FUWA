namespace FishUsaWebApp.Models.ViewModels.CommonModels;

public sealed class ObjectDetailsViewModel
{
    public object? Source { get; set; }
    public string Title { get; set; } = "Details";
    public IReadOnlyList<string>? IncludedFields { get; set; }
    public List<ObjectDetailsFieldViewModel> Fields { get; set; } = [];
}

public sealed class ObjectDetailsFieldViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string? Value { get; set; }
    public bool IsEmpty { get; set; }
    public bool IsHeader { get; set; }
}