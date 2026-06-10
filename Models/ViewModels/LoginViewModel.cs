using System.ComponentModel.DataAnnotations;

namespace FishUsaWebApp.Models.ViewModels;

public sealed class LoginViewModel
{
    [Required(ErrorMessage = "Enter your employee code.")]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Employee code must be 4 digits.")]
    public string EmployeeCode { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
}