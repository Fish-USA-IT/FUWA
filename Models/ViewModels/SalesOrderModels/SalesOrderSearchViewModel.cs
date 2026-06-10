using FishUsaWebApp.Models.SuiteQlRows;
using FishUsaWebApp.Models.ViewModels.CommonModels;
using FishUsaWebApp.Models.ViewModels.SearchFilterModels;

namespace FishUsaWebApp.Models.ViewModels.SalesOrderModels;

public sealed class SalesOrderSearchViewModel
    : SearchViewModelBase<SalesOrderSearchSuiteQlRow>
{
    public SalesOrderSearchViewModel()
    {
        SearchFilters = SearchFilterDefinitions.SalesOrder;

        ActiveFilters =
        [
            new SearchFilterRowViewModel
            {
                Filter = SearchFilters.FirstOrDefault()?.Value,
                Value = string.Empty
            }
        ];
    }
}