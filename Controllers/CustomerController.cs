using FishUsaWebApp.Models.SuiteQlRows;
using FishUsaWebApp.Models.ViewModels.CommonModels;
using FishUsaWebApp.Models.ViewModels.CustomerModels;
using FishUsaWebApp.SuiteQlQueries;
using Microsoft.AspNetCore.Mvc;
using NetSuite.SchemaSdk.Client;
using NetSuite.SchemaSdk.Models;
using NetSuite.SchemaSdk.Records;

namespace FishUsaWebApp.Controllers;

public class CustomerController : AppController
{
    private readonly NetSuiteRestClient _netSuiteClient;

    public CustomerController(NetSuiteRestClient netSuiteClient)
    {
        _netSuiteClient = netSuiteClient;
    }

    [HttpGet]
    public async Task<IActionResult> Search(
        List<SearchFilterRowViewModel>? activeFilters,
        int page = 1,
        int pageSize = 25)
    {
        if (!IsLoggedIn())
        {
            return RedirectToLogin();
        }

        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 10, 25);

        activeFilters = NormalizeFilters(activeFilters);

        var model = new CustomerSearchViewModel
        {
            ActiveFilters = activeFilters,
            Page = page,
            PageSize = pageSize
        };

        model.HasSearched = activeFilters.Any(x =>!string.IsNullOrWhiteSpace(x.Value));

        if (!model.HasSearched)
        {
            return View("CustomerSearchView", model);
        }

        var results = await _netSuiteClient.QuerySuiteQlAsync<
            SuiteQlResponse<CustomerSearchSuiteQlRow>>(
                CustomerQueries.SearchCustomers(
                    activeFilters,
                    page,
                    pageSize + 1));

        var items = results?.Items ?? [];

        model.HasNextPage = items.Count > pageSize;
        model.Results = items.Take(pageSize).ToList();

        return View("CustomerSearchView", model);
    }

    [HttpGet]
    public async Task<IActionResult> View(string id)
    {
        if (!IsLoggedIn())
        {
            return RedirectToLogin();
        }

        var customer = await _netSuiteClient.GetRecordAsync<Customer>(
            NetSuiteRecordTypes.Customer,
            id);

        var model = new CustomerDetailsViewModel
        {
            Customer = customer
        };

        return View("CustomerView", model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!IsLoggedIn())
        {
            return RedirectToLogin();
        }

        return View();
    }

    private static List<SearchFilterRowViewModel> NormalizeFilters(
        List<SearchFilterRowViewModel>? activeFilters)
    {
        activeFilters ??= [];

        activeFilters = activeFilters
            .Where(x => !string.IsNullOrWhiteSpace(x.Filter)
                     && !string.IsNullOrWhiteSpace(x.Value))
            .ToList();

        if (activeFilters.Count == 0)
        {
            activeFilters.Add(new SearchFilterRowViewModel
            {
                Filter = "customer_id",
                Value = string.Empty
            });
        }

        return activeFilters;
    }
}