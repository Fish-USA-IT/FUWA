using Microsoft.AspNetCore.Mvc.Rendering;

namespace FishUsaWebApp.Models.ViewModels.SearchFilterModels;

public sealed class SearchFilterDefinition
{
    public string Value { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public string Placeholder { get; set; } = "Enter search value";

    public SelectListItem ToSelectListItem()
    {
        return new SelectListItem
        {
            Value = Value,
            Text = Text
        };
    }
}