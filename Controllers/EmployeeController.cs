using FishUsaWebApp.Models.ViewModels.EmployeeModels;
using Microsoft.AspNetCore.Mvc;
using NetSuite.SchemaSdk.Client;
using NetSuite.SchemaSdk.Models;
using NetSuite.SchemaSdk.Records;

namespace FishUsaWebApp.Controllers;

public class EmployeeController : AppController
{
    private readonly NetSuiteRestClient _netSuiteClient;

    public EmployeeController(NetSuiteRestClient netSuiteClient)
    {
        _netSuiteClient = netSuiteClient;
    }

    [HttpGet]
    public IActionResult Search()
    {
        return View(new EmployeeDetailsViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> ViewLoggedIn()
    {
        var employeeId = HttpContext.Session.GetString("EmployeeId");

        if (string.IsNullOrWhiteSpace(employeeId))
        {
            return RedirectToLogin();
        }

        var employee = await _netSuiteClient.GetRecordAsync<Employee>(NetSuiteRecordTypes.Employee, employeeId);

        var model = new EmployeeDetailsViewModel
        {
            Employee = employee
        };

        return View(model);
    }
}