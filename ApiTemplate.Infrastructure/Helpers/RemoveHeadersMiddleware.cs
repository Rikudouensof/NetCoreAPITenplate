using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Infrastructure.Helpers
{
    public class RemoveServerHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public RemoveServerHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Remove("Server");
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }

}
