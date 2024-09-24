using ApiTemplate.Application.Helpers;
using ApiTemplate.Infrastructure.Helpers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Infrastructure.DependencyAbstraction
{
    public static class CoreDependencies
    {
        public static IServiceCollection ImplementCoreDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerHelper, LoggerHelper>();
            services.PostConfigure<KestrelServerOptions>(options =>
            {
                options.AddServerHeader = false;
            });

            return services;
        }
    }
}