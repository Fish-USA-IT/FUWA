using FishUsaWebApp.Models.ViewModels.CommonModels;
using FishUsaWebApp.Models.ViewModels.SearchFilterModels;

public abstract class SearchViewModelBase<TResult>
{
    public List<SearchFilterDefinition> SearchFilters { get; set; } = [];

    public List<SearchFilterRowViewModel> ActiveFilters { get; set; } =
    [
        new()
    ];

    public List<TResult> Results { get; set; } = [];

    public bool HasSearched { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 25;

    public bool HasNextPage { get; set; }

    public bool HasPreviousPage => Page > 1;

    public int ResultCount => Results.Count;
}