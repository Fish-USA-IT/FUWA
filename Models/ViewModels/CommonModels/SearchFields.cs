namespace FishUsaWebApp.Models.ViewModels.CommonModels;

public static class SearchFields
{
    public static List<SearchResultColumnViewModel> Customers =>
    [
        new() { Header = "Customer", PropertyName = "DisplayName" },
        new() { Header = "Entity ID", PropertyName = "EntityId" },
        new() { Header = "Email", PropertyName = "Email" },
        new() { Header = "Phone", PropertyName = "Phone" }
    ];

    public static List<SearchResultColumnViewModel> SalesOrders =>
    [
        new() { Header = "Order #", PropertyName = "TranId" },
        new() { Header = "Date", PropertyName = "TranDate" },
        new() { Header = "Customer", PropertyName = "CustomerName" },
        new() { Header = "Status", PropertyName = "Status" },
        new() { Header = "Total", PropertyName = "Total" }
    ];

    public static List<SearchResultColumnViewModel> Employees =>
    [
        new() { Header = "Employee", PropertyName = "DisplayName" },
        new() { Header = "Entity ID", PropertyName = "EntityId" },
        new() { Header = "Email", PropertyName = "Email" },
        new() { Header = "Title", PropertyName = "Title" }
    ];
}