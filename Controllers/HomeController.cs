using Microsoft.AspNetCore.Mvc;
using NetSuite.SchemaSdk.Client;
using NetSuite.SchemaSdk.Models;
using FishUsaWebApp.Models.ViewModels;
using FishUsaWebApp.SuiteQlQueries;

namespace FishUsaWebApp.Controllers;

public class HomeController : AppController
{
    private readonly NetSuiteRestClient _netSuiteClient;
    private readonly ILogger<HomeController> _logger;
    private readonly string _netSuiteAccountId;

    public HomeController(
        NetSuiteRestClient netSuiteClient,
        ILogger<HomeController> logger,
        IConfiguration config)
    {
        _netSuiteClient = netSuiteClient;
        _logger = logger;
        _netSuiteAccountId = config["NetSuite:AccountId"] ?? throw new InvalidOperationException("Missing NetSuite:AccountId config value.");
    }

    public async Task<IActionResult> Index()
    {
        var loggedInEmployeeId = HttpContext.Session.GetString("EmployeeId");

        if (string.IsNullOrWhiteSpace(loggedInEmployeeId))
        {
            return RedirectToAction("Login", "Auth");
        }

        var sql = EmployeeQueries.GetEmployeeById(loggedInEmployeeId);
        var employees = await _netSuiteClient.QuerySuiteQlAsync<EmployeeCollection>(sql);
        var loggedInEmployee = employees?.Items?.FirstOrDefault();

        var model = new HomeNetSuiteViewModel
        {
            LoggedInEmployee = loggedInEmployee,
        };

        return View(model);
    }
}