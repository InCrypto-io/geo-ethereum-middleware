using System;
using System.Net;
using System.Threading.Tasks;
using GeoEthereumMiddleware.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GeoEthereumMiddleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.BadRequest; 

            var result = JsonConvert.SerializeObject(new ExceptionResponce()
            {
               ErrorMessage = exception.Message,
               Status = StatusMessageEnum.BAD 
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    public class ExceptionResponce : BaseResponse
    {
        public string ErrorMessage { get; set; }
    }
}