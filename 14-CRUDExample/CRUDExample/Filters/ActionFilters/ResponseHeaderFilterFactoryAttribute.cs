using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ActionFilters;

public class ResponseHeaderFilterFactoryAttribute(string key, string value, int order) : Attribute, IFilterFactory
{
    public bool IsReusable => false;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<ResponseHeaderActionFilter>>();
        var filter = new ResponseHeaderActionFilter(logger, key, value, order);
        return filter;
    }
}