using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApiCsharp.Api.Controllers.Contracts;
using ApiCsharp.Api.Core.Domain.Shared.Exceptions;

namespace ApiCsharp.Api.Controllers.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                var statusCode = exception switch
                {
                    NotFoundException => (int) HttpStatusCode.NotFound,
                    _ => (int) HttpStatusCode.InternalServerError
                };
                
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsJsonAsync(new ResponseDto(exception));
            }
        }
    }
}