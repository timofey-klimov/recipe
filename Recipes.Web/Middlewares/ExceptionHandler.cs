using Recipes.Application.Shared.Exceptions;
using Recipes.Domain.Exceptions;
using Recipes.Domain.Exceptions.Core;

namespace Recipes.Web.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
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
            }
        }

        #region handlers
        private async Task HandleInternalError(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new
            {
                Title = "Internal Server Error"
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
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    var buisnessErrorresponse = new
                    {
                        Title = "Buisness Error",
                        Description = buisnessError.GetDescription(),
                        Code = buisnessError.GetCode()
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
                Errors = validationException.Errors
            };
            await context.Response.WriteAsJsonAsync(response);

        }
        #endregion
    }
}
