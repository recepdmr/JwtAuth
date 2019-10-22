using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace JwtAuth
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //logging
                //_loggerService.Error(ex);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync(ex.Message);
            }
        }
    }
}
