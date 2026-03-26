using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace BandScope.Server.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ObjectResult result;

            if (context.Exception is ValidationException)
            {
                Log.Logger.Warning(context.Exception, "ValidationException occured");
                result = new ObjectResult(new { message = context.Exception.Message });
                result.StatusCode = 400;
            }
            else
            {
                Log.Logger.Error(context.Exception, "An internal server Error has occurred");
                result = new ObjectResult(new { message = "internalServerErrorOccured" });
                result.StatusCode = 500;
            }

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
