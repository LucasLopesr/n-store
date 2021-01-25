using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.CustomExceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
                await next(httpContext);
            }
            catch (CustomHttpRequestException cx) 
            {
                HandlwRequestExceptionAsync(httpContext, cx);
            }
        }

        private static void HandlwRequestExceptionAsync(HttpContext httpContext, CustomHttpRequestException httpRequestException) 
        { 
            if (httpRequestException.StatusCode == HttpStatusCode.Unauthorized) 
            {
                httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int)httpRequestException.StatusCode;

        }
    }
}
