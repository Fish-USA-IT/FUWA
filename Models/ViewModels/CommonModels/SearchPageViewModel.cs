using FishUsaWebApp.Models.ViewModels.SearchFilterModels;

namespace FishUsaWebApp.Models.ViewModels.CommonModels;

public sealed class SearchPageViewModel
{
    public string Title { get; set; } = "Search";
    public string Description { get; set; } = string.Empty;

    public string Controller { get; set; } = string.Empty;
    public string Action { get; set; } = "Search";

    public List<SearchFilterDefinition> SearchFilters { get; set; } = [];

    public List<SearchFilterRowViewModel> ActiveFilters { get; set; } =
    [
        new()
    ];

    public bool HasSearched { get; set; }

    public object? Source { get; set; }

    public List<SearchResultColumnViewModel> Columns { get; set; } = [];

    public string ViewController { get; set; } = string.Empty;
    public string ViewAction { get; set; } = "View";
    public string IdPropertyName { get; set; } = "Id";

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage => Page > 1;
}

public sealed class SearchResultColumnViewModel
{
    public string Header { get; set; } = string.Empty;

    public string PropertyName { get; set; } = string.Empty;
}