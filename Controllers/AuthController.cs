using FishUsaWebApp.Models.SuiteQlRows;
using FishUsaWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NetSuite.SchemaSdk.Client;

namespace FishUsaWebApp.Controllers;

public class AuthController : AppController
{
    private readonly NetSuiteRestClient _netSuiteClient;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        NetSuiteRestClient netSuiteClient,
        ILogger<AuthController> logger)
    {
        _netSuiteClient = netSuiteClient;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var employeeCode = model.EmployeeCode.Trim();

        var result = await _netSuiteClient.QuerySuiteQlAsync<SuiteQlResponse<EmployeeLoginSuiteQlRow>>(
            $@"
                SELECT
                    id,
                    entityid,
                    firstname,
                    lastname,
                    email
                FROM employee
                WHERE custentity_warehouse_app_password = '{employeeCode}'
                    AND isinactive = 'F'");

        var employee = result?.Items.FirstOrDefault();

        if (employee?.Id is null)
        {
            model.ErrorMessage = "Invalid employee code.";
            return View(model);
        }

        HttpContext.Session.SetString("EmployeeId", employee.Id);
        HttpContext.Session.SetString("EmployeeEntityId", employee.EntityId ?? string.Empty);
        HttpContext.Session.SetString("EmployeeFirstName", employee.FirstName ?? string.Empty);
        HttpContext.Session.SetString("EmployeeLastName", employee.LastName ?? string.Empty);

        return RedirectToAction("Index", "Home");
    }
}