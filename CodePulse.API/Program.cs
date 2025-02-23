
using CodePulse.API.Data;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services for controllers
            builder.Services.AddControllers();

            // Add & Configure API endpoints
            builder.Services.AddEndpointsApiExplorer();

            // Add & Configure Swagger
            builder.Services.AddSwaggerGen();


            // inject DbContect Into Application
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CodePulseConnectionString"));
            });

            // inject repository into Application
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
               app.UseSwagger();
               app.UseSwaggerUI( c =>
               {
                   c.SwaggerEndpoint("swagger/v1/swagger.json", "API V1");
                   c.RoutePrefix = string.Empty;
               });
            }

            app.UseHttpsRedirection();

            // Add & Setup CORS Policy
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
            });

            app.MapControllers();

            app.Run();
        }
    }
}
