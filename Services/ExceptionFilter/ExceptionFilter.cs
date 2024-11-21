using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Exceptions;

namespace WebApplication1.Services.ExceptionHandler
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            var exception = context.Exception;

            switch (exception)
            {
                case ControllerInModelException e:
                    {
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        await response.WriteAsJsonAsync(e.Errors);
                        break;
                    }
                default:
                    {
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                    }
            }

            context.ExceptionHandled = true;
        }
    }
}
