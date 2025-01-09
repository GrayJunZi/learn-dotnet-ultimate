using Microsoft.AspNetCore.Mvc;

namespace ViewComponentsExample.ViewComponents;

public class GridViewComponent : ViewComponent
{
   public Task<IViewComponentResult> InvokeAsync()
   {
       // 调用分部视图
       // Views\Shared\Components\Grid\Default.cshtml
       return View();
   }
}