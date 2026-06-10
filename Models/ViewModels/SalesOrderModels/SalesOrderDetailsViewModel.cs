using FishUsaWebApp.Models.SuiteQlRows;

namespace FishUsaWebApp.Models.ViewModels.SalesOrderModels;

public sealed class SalesOrderDetailsViewModel
{
    public SalesOrderSearchSuiteQlRow? SalesOrder { get; set; }

    public List<SalesOrderItemLineSuiteQlRow> ItemLines { get; set; } = [];
}