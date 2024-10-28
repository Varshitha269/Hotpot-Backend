using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1
{
    public class ActionFilter :IActionFilter
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ActionFilter));

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Log the start of the action method
        var methodName = context.ActionDescriptor.DisplayName;
        var httpMethod = context.HttpContext.Request.Method;

        _logger.Info($"Action method '{methodName}' (HTTP Method: {httpMethod}) started.");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Log the end of the action method
        var methodName = context.ActionDescriptor.DisplayName;
        var httpMethod = context.HttpContext.Request.Method;

        _logger.Info($"Action method '{methodName}' (HTTP Method: {httpMethod}) ended.");
    }
}
}
