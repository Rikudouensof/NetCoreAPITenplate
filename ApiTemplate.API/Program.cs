
using ApiTemplate.Infrastructure.Helpers;

namespace ApiTemplate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
