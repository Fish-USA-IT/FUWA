using FishUsaWebApp.Models.SuiteQlRows;
using FishUsaWebApp.Models.ViewModels.CommonModels;
using FishUsaWebApp.Models.ViewModels.SearchFilterModels;

namespace FishUsaWebApp.Models.ViewModels.CustomerModels;

public sealed class CustomerSearchViewModel
    : SearchViewModelBase<CustomerSearchSuiteQlRow>
{
    public CustomerSearchViewModel()
    {
        SearchFilters = SearchFilterDefinitions.Customer;

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