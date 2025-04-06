using Challenge.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Bff.Controllers.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        private readonly IWebHostEnvironment _env;
        public ProblemDetailsFactory ProblemDetailsFactory { get; }

        public HttpResponseExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception != null && context.Exception is NotFoundExceptions)
            {
                context.Result = new NotFoundResult();
                context.ExceptionHandled = true;
                return;
            }
            if (ShouldConvertToProblemDetails(context))
            {
                string detailMessage = null;
                if (context.Exception != null)
                    detailMessage = context.Exception.Message;
                else if ((context.Result is ObjectResult resultObject) && (resultObject.Value is Exception resultExceptionValue))
                    detailMessage = resultExceptionValue.Message;
                else if ((context.Result is ObjectResult resultObjectString) && (resultObjectString.Value is String text))
                    detailMessage = text;

                ProblemDetails problemDetails = this.ProblemDetailsFactory.CreateProblemDetails(context.HttpContext, statusCode: 400, detail: detailMessage, title: "Erro", type: "error");
                context.Result = new ObjectResult(problemDetails);
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        bool ShouldConvertToProblemDetails(ActionExecutedContext context)
        {
            if (_env.IsDevelopment())
                return false;
            return ((context.Result is ObjectResult objResult) && (objResult.Value is not ProblemDetails) && objResult.StatusCode.HasValue && objResult.StatusCode >= 400 && objResult.StatusCode <= 499) || (context.Exception != null);
        }
    }
}
