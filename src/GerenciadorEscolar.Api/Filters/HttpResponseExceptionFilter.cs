using Microsoft.AspNetCore.Mvc.Filters;
using GerenciadorEscolar.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using GerenciadorEscolar.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GerenciadorEscolar.Api.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        public IWebHostEnvironment Environment { get; }

        public HttpResponseExceptionFilter(IWebHostEnvironment env)
        {
            Environment = env;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is AppException exception)
                {
                    context.Result = new ObjectResult(new OperacaoCrudDto()
                    {
                        Success = false,
                        Error = exception.Message
                    })
                    {
                        StatusCode = exception.StatusCode
                    };
                    context.ExceptionHandled = true;
                }
                else
                {
                    context.Result = new ObjectResult(new OperacaoCrudDto()
                    {
                        Success = false,
                        Error = Environment.IsDevelopment() ? context.Exception.Message : context.Exception.Message
                    })
                    {
                        StatusCode = 500
                    };

                    System.Diagnostics.Trace.TraceError("Erro filtrado: " + context.Exception.Message);
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}