using Microsoft.AspNetCore.Mvc.Filters;

namespace PayrolAPI.Filters
{
    public class MyLoggingAsync : Attribute, IAsyncActionFilter
    {
        private readonly string _callerName;

        public MyLoggingAsync(string callerName)
        {
            _callerName = callerName;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"Async - Before the request execute {_callerName}");
            await next();
            Console.WriteLine($"Async - After the request execute {_callerName}");
        }
    }
}
