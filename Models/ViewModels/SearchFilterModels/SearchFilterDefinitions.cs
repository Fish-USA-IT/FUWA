namespace FishUsaWebApp.Models.ViewModels.SearchFilterModels;

public static class SearchFilterDefinitions
{
    public static readonly List<SearchFilterDefinition> Customer =
    [
        new()
        {
            Value = "customer_id",
            Text = "Customer ID",
            Placeholder = "Enter Customer Internal ID"
        },
        new()
        {
            Value = "entity_id",
            Text = "Entity ID",
            Placeholder = "Enter Customer Entity ID"
        },
        new()
        {
            Value = "email",
            Text = "Email",
            Placeholder = "Enter Customer Email"
        },
        new()
        {
            Value = "phone",
            Text = "Phone",
            Placeholder = "Enter Customer Phone Number"
        }
    ];

    public static readonly List<SearchFilterDefinition> SalesOrder =
    [
        new()
        {
            Value = "so_number",
            Text = "SO Number",
            Placeholder = "Enter Sales Order Number"
        },
        new()
        {
            Value = "bigcommerce_number",
            Text = "BigCommerce Number",
            Placeholder = "Enter BigCommerce Order Number"
        },
        new()
        {
            Value = "customer_email",
            Text = "Customer Email",
            Placeholder = "Enter Customer Email"
        },
        new()
        {
            Value = "customer_name",
            Text = "Customer Name",
            Placeholder = "Enter Customer Name"
        },
        new()
        {
            Value = "customer_phone",
            Text = "Customer Phone Number",
            Placeholder = "Enter Customer Phone Number"
        }
    ];
}