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


        public static IServiceCollection ImplementCoreDependencies(this IServiceCollection service)
        {
            

            return service;
        }
    }
}