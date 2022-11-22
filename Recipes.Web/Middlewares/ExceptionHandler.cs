using Recipes.Application.Shared.Exceptions;
using Recipes.Domain.Exceptions;
using Recipes.Domain.Exceptions.Core;

namespace Recipes.Web.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandler> _logger;
        public ExceptionHandler(RequestDelegate requestDelegate, ILogger<ExceptionHandler> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (AppValidationException validationException)
            {
                await HandleValidationException(validationException, context);
            }
            catch (DomainException domainException)
            {
                await HandleDomainException(domainException, context);
            }
            catch (Exception ex)
            {
                await HandleInternalError(ex, context);
                _logger.LogError(ex.Message);
            }
        }

        #region handlers
        private async Task HandleInternalError(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new
            {
                Title = "Internal Server Error",
                Success = false
            };
            await context.Response.WriteAsJsonAsync(response);
        }
        private async Task HandleDomainException(DomainException domainException, HttpContext context)
        {
            switch (domainException)
            {
                case NotFoundDomainException notFound:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    var notFoundresponse = new
                    {
                        Title = "Not found",
                        Description = notFound.Message
                    };
                    await context.Response.WriteAsJsonAsync(notFoundresponse);
                    break;
                case BuisnessErrorDomainException buisnessError:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    var buisnessErrorresponse = new
                    {
                        Title = "Buisness Error",
                        Description = buisnessError.GetDescription(),
                        Code = buisnessError.GetCode(),
                        Success = false
                    };
                    await context.Response.WriteAsJsonAsync(buisnessErrorresponse);
                    break;
            }
        }
        private async Task HandleValidationException(AppValidationException validationException, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var response = new
            {
                Title = "Validation failure",
                Description = "Validation errors has been occured",
                Errors = validationException.Errors,
                Success = false
            };
            await context.Response.WriteAsJsonAsync(response);

        }
        #endregion
    }
}
