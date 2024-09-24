using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using ApiTemplate.Infrastructure.Helpers.Validatiors;

namespace ApiTemplate.Infrastructure.DependencyAbstraction
{
    public static class ValidatorDependencies
    {
        public static IServiceCollection ImplementFluentValidations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<LoginModelValidatior>();
            return services;
        }
    }
}
