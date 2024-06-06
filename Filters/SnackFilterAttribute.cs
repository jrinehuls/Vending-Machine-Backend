using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VendingMachine.Exceptions;
using VendingMachine.Models.DTOs;

namespace VendingMachine.Filters
{
    public class SnackFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException apiException)
            {
                ErrorResponse errorResponse = new(apiException.Message);
                ObjectResult result = new(errorResponse)
                {
                    StatusCode = apiException.StatusCode
                };
                context.Result = result;
            }
        }
    }
}
