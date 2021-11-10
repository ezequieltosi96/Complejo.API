using Complejo.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Complejo.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            
            var result = string.Empty;

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new { errors = validationException.ValidationErrors, StatusCode = httpStatusCode });
                    break;

                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;

                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;

                case Exception ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if(result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message, statusCode = httpStatusCode });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
