
using CodePulse.API.Data;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace CodePulse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services for controllers
            builder.Services.AddControllers();

            // add http context accessor
            builder.Services.AddHttpContextAccessor();

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
            builder.Services.AddScoped<IImageRepository, ImageRepository>();

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

            // enable serve static images from API
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });

            app.MapControllers();

            app.Run();
        }
    }
}
