using Microsoft.AspNetCore.Mvc;

namespace ViewComponentsExample.ViewComponents;

public class GridViewComponent : ViewComponent
{
   public async Task<IViewComponentResult> InvokeAsync()
   {
       // 默认视图路径 Views\Shared\Components\Grid\Default.cshtml
       // return View();

       return View("Sample");
   }
}