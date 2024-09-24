using ApiTemplate.Infrastructure.DependencyAbstraction;
using ApiTemplate.Infrastructure.Helpers;
using NLog;
using NLog.Extensions.Logging;

namespace ApiTemplate.ScreamAPI
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
            //Inegrate Core Layer Dependencies
            builder.Services.ImplementCoreDependencies();

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                await next();
            });

            // Use the CORS policy globaly
            app.UseCors("MyAllowSpecificOrigins");
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseMiddleware<RemoveServerHeaderMiddleware>();
            app.MapControllers();
            app.UseHsts();
            app.Run();
        }
    }
}
