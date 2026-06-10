using FishUsaWebApp.Models.ViewModels.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Reflection;
using System.Text.Json;

namespace FishUsaWebApp.ViewComponents;

public class ObjectDetailsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(
    object? source,
    string title = "Details",
    IReadOnlyList<string>? includedFields = null)
    {
        var model = new ObjectDetailsViewModel
        {
            Source = source,
            Title = title,
            IncludedFields = includedFields
        };

        if (source is null)
        {
            return View(model);
        }

        var fields = GetDisplayFields(source);

        if (includedFields is not null && includedFields.Count > 0)
        {
            foreach (var fieldName in includedFields)
            {
                if (fieldName.StartsWith("#") && fieldName.EndsWith("#"))
                {
                    var headerText = fieldName.Trim('#').Trim();

                    model.Fields.Add(new ObjectDetailsFieldViewModel
                    {
                        Name = headerText,
                        Label = headerText,
                        Value = headerText,
                        IsHeader = true
                    });

                    continue;
                }

                if (string.IsNullOrWhiteSpace(fieldName))
                {
                    model.Fields.Add(new ObjectDetailsFieldViewModel
                    {
                        Name = string.Empty,
                        Label = string.Empty,
                        Value = string.Empty,
                        IsEmpty = true
                    });

                    continue;
                }

                var value = fieldName.Contains('.') ? GetNestedValue(source, fieldName) 
                    : fields.TryGetValue(fieldName, out var directValue)
                        ? directValue
                        : null;

                if (value is null)
                {
                    continue;
                }

                model.Fields.Add(new ObjectDetailsFieldViewModel
                {
                    Name = fieldName,
                    Label = ToDisplayLabel(fieldName),
                    Value = FormatValue(value)
                });
            }
        }
        else
        {
            model.Fields = fields
                .OrderBy(field => field.Key)
                .Select(field => new ObjectDetailsFieldViewModel
                {
                    Name = field.Key,
                    Label = ToDisplayLabel(field.Key),
                    Value = FormatValue(field.Value)
                })
                .Where(field => !string.IsNullOrWhiteSpace(field.Value))
                .ToList();
        }

        return View(model);
    }

    private static string ToDisplayLabel(string propertyName)
    {
        var label = string.Empty;

        foreach (var character in propertyName)
        {
            if (char.IsUpper(character) && label.Length > 0)
            {
                label += " ";
            }

            label += character;
        }

        return label;
    }

    private static string? FormatValue(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return null;
            }

            return stringValue switch
            {
                "T" => "Yes",
                "F" => "No",
                _ => stringValue
            };
        }

        if (value is DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy");
        }

        if (value is bool boolean)
        {
            return boolean ? "Yes" : "No";
        }

        if (value is IEnumerable enumerable && value is not string)
        {
            var items = enumerable.Cast<object?>().ToList();

            if (items.Count == 0)
            {
                return "[]";
            }

            return JsonSerializer.Serialize(
                items,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });
        }

        var type = value.GetType();

        if (!type.IsPrimitive && !type.IsEnum && type != typeof(decimal))
        {
            return JsonSerializer.Serialize(
                value,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });
        }

        if (value is JsonElement jsonElement)
        {
            return jsonElement.ValueKind switch
            {
                JsonValueKind.String => jsonElement.GetString(),
                JsonValueKind.Number => jsonElement.ToString(),
                JsonValueKind.True => "Yes",
                JsonValueKind.False => "No",
                JsonValueKind.Null => null,
                JsonValueKind.Undefined => null,
                _ => JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions
                {
                    WriteIndented = true
                })
            };
        }

        return value.ToString();
    }

    private static object? GetNestedValue(object? source, string fieldPath)
    {
        if (source is null || string.IsNullOrWhiteSpace(fieldPath))
        {
            return null;
        }

        object? currentValue = source;

        foreach (var part in fieldPath.Split('.', StringSplitOptions.RemoveEmptyEntries))
        {
            if (currentValue is null)
            {
                return null;
            }

            var property = currentValue.GetType().GetProperty(part);

            if (property is null)
            {
                return null;
            }

            currentValue = property.GetValue(currentValue);
        }

        return currentValue;
    }

    private static bool ShouldDisplayProperty(PropertyInfo property)
    {
        if (property.Name.Equals("Links", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (property.Name.Equals("custentity_warehouse_app_password", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        if (type == typeof(string) ||
            type == typeof(int) ||
            type == typeof(long) ||
            type == typeof(decimal) ||
            type == typeof(double) ||
            type == typeof(float) ||
            type == typeof(bool) ||
            type == typeof(DateTime))
        {
            return true;
        }

        return false;
    }

    private static Dictionary<string, object?> GetDisplayFields(object source)
    {
        var fields = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

        var properties = source.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(property => property.GetIndexParameters().Length == 0);

        foreach (var property in properties)
        {
            if (property.Name.Equals("AdditionalProperties", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var value = property.GetValue(source);

            if (value is not null)
            {
                fields[property.Name] = value;
            }
        }

        var additionalPropertiesProperty = source.GetType()
            .GetProperty("AdditionalProperties");

        var additionalProperties =
            additionalPropertiesProperty?.GetValue(source)
            as Dictionary<string, JsonElement>;

        if (additionalProperties is not null)
        {
            foreach (var item in additionalProperties)
            {
                fields[item.Key] = item.Value;
            }
        }

        return fields;
    }
}