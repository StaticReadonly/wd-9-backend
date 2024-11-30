using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Exceptions;

namespace WebApplication1.Services.ExceptionHandler
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            var exception = context.Exception;

            _logger.LogError(exception, exception.Message);

            switch (exception)
            {
                case ControllerInModelException e:
                    {
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        await response.WriteAsJsonAsync(e.Errors);
                        break;
                    }
                case EntityNotFoundException e:
                    {
                        response.StatusCode = StatusCodes.Status404NotFound;
                        await response.WriteAsync(e.Message);
                        break;
                    }
                case AccessDeniedException e:
                    {
                        response.StatusCode = StatusCodes.Status403Forbidden;
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
