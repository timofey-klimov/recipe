using Recipes.Application.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        #region handlers
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
