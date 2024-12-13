using System.Text.RegularExpressions;

namespace RoutingExample.CustomConstraints;

public class MonthsCustomConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        // 检查值是否存在
        if (!values.ContainsKey(routeKey))
        {
            return false;
        }
        
        Regex regex = new(@"^(apr|jul|oct|jan)$");
        
        var monthValue = values[routeKey].ToString();
        
        return regex.IsMatch(monthValue);
    }
}