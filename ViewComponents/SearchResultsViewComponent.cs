using System.Collections;
using System.Reflection;
using FishUsaWebApp.Models.ViewModels.CommonModels;
using Microsoft.AspNetCore.Mvc;

namespace FishUsaWebApp.ViewComponents;

public class SearchResultsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(SearchPageViewModel model)
    {
        return View(model);
    }

    public static List<object> GetRows(object? source)
    {
        if (source is null)
        {
            return [];
        }

        if (source is IEnumerable enumerable && source is not string)
        {
            return enumerable
                .Cast<object>()
                .ToList();
        }

        return [source];
    }

    public static string? GetPropertyValue(object source, string propertyName)
    {
        if (source is null || string.IsNullOrWhiteSpace(propertyName))
        {
            return null;
        }

        object? currentValue = source;

        foreach (var part in propertyName.Split('.', StringSplitOptions.RemoveEmptyEntries))
        {
            if (currentValue is null)
            {
                return null;
            }

            var property = currentValue.GetType()
                .GetProperty(
                    part,
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase);

            if (property is null)
            {
                return null;
            }

            currentValue = property.GetValue(currentValue);
        }

        return currentValue?.ToString();
    }
}