using System.Net;
using Microsoft.AspNetCore.Mvc;
using ApiCsharp.Api.Controllers.Contracts;

namespace ApiCsharp.Api.Controllers.Extensions
{
    public static class ResponseExtensionMethod
    {
        public static IActionResult AsResponse(this object data, HttpStatusCode statusCode)
        {
            return new ObjectResult(new ResponseDto(data))
            {
                StatusCode = (int) statusCode
            };
        }
    }
}