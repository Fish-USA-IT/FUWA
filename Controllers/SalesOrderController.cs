using FishUsaWebApp.Models.SuiteQlRows;
using FishUsaWebApp.Models.ViewModels.CommonModels;
using FishUsaWebApp.Models.ViewModels.SalesOrderModels;
using FishUsaWebApp.SuiteQlQueries;
using Microsoft.AspNetCore.Mvc;
using NetSuite.SchemaSdk.Client;

namespace FishUsaWebApp.Controllers;

public class SalesOrderController : AppController
{
    private readonly NetSuiteRestClient _netSuiteClient;

    public SalesOrderController(NetSuiteRestClient netSuiteClient)
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

        var model = new SalesOrderSearchViewModel
        {
            ActiveFilters = activeFilters,
            Page = page,
            PageSize = pageSize
        };

        model.HasSearched = activeFilters.Any(x => !string.IsNullOrWhiteSpace(x.Value));

        if (!model.HasSearched)
        {
            return View("SalesOrderSearchView", model);
        }

        var results = await _netSuiteClient.QuerySuiteQlAsync<
            SuiteQlResponse<SalesOrderSearchSuiteQlRow>>(
                SalesOrderQueries.SearchSalesOrders(
                    activeFilters,
                    page,
                    pageSize + 1));

        var items = results?.Items ?? [];

        model.HasNextPage = items.Count > pageSize;
        model.Results = items.Take(pageSize).ToList();

        return View("SalesOrderSearchView", model);
    }

    [HttpGet]
    public async Task<IActionResult> View(string id)
    {
        if (!IsLoggedIn())
        {
            return RedirectToLogin();
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            return RedirectToAction("Search");
        }

        var salesOrderResult = await _netSuiteClient.QuerySuiteQlAsync<
            SuiteQlResponse<SalesOrderSearchSuiteQlRow>>(
                SalesOrderQueries.GetSalesOrderById(id));

        var itemLineResult = await _netSuiteClient.QuerySuiteQlAsync<
            SuiteQlResponse<SalesOrderItemLineSuiteQlRow>>(
                SalesOrderQueries.GetSalesOrderItemLinesBySalesOrderId(id));

        var model = new SalesOrderDetailsViewModel
        {
            SalesOrder = salesOrderResult?.Items.FirstOrDefault(),
            ItemLines = itemLineResult?.Items ?? []
        };

        return View("SalesOrderView", model);
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
                Filter = "so_number",
                Value = string.Empty
            });
        }

        return activeFilters;
    }
}