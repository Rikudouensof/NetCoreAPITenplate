using ApiTemplate.Infrastructure.Helpers;
using ApiTemplate.Infrastructure.DependencyAbstraction;
using NLog.Extensions.Logging;
using NLog;
using FluentValidation;
using FluentValidation.AspNetCore;
using ApiTemplate.Application.Helpers;
using ApiTemplate.API.APIServices;

namespace ApiTemplate.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            NLogProviderOptions nlpopts = new NLogProviderOptions
            {
                IgnoreEmptyEventId = true,
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true,
                ParseMessageTemplates = true,
                IncludeScopes = true,
                ShutdownOnDispose = true
            };


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IAppsettingGeneratorHelper, AppsettingGeneratorHelper>();


            //Ensure dependecny setup
            var appsettingGeneratorHelper = builder.Services.BuildServiceProvider().GetRequiredService<IAppsettingGeneratorHelper>();
            CoreDependencies.Configure(appsettingGeneratorHelper);

            //Inegrate Core Layer Dependencies
            builder.Services.ImplementCoreDependencies();
            builder.Services.ImplementFluentValidations();


            //Handle Logging system
            builder.Services.AddLogging(
               builder =>
               {
                   builder.AddConsole().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   builder.AddNLog(nlpopts);
               });
            LogManager.Setup().LoadConfigurationFromFile();
            NLogLoggerProvider nlogProv = new NLogLoggerProvider(nlpopts);
            ILoggerProvider castLoggerProvider = nlogProv as ILoggerProvider;
            builder.Services.AddSingleton<ILoggerProvider>(castLoggerProvider);

            
            

            //Add Cors system
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("https://example.com")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            //Hide Header Params
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Add("server", "hidden");
                context.Response.Headers.Add("date", "hidden");
                await next();
            });

            // Use the CORS policy globaly
            app.UseCors("MyAllowSpecificOrigins");


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<RemoveServerHeaderMiddleware>();
            app.MapControllers();
            app.UseHsts();
            app.Run();
        }
    }
}
