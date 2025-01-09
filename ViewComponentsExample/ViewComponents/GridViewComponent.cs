using Microsoft.AspNetCore.Mvc;
using ViewComponentsExample.Models;

namespace ViewComponentsExample.ViewComponents;

public class GridViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new PersonGridModel()
        {
            GridTitle = "Person Grid",
            Persons = Enumerable.Range(1, 10).Select(x => new Person
            {
                Name = $"ROBOT {58720+x}",
                JobTitle = x % Random.Shared.Next(2,10) == 0 ? "Manager" : "Employee"
            }).ToList(),
        };
        ViewData["Grid"] = model;
        return View("Sample");
    }
}