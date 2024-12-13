using libAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace libAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Build and run the host
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        // Add DbContext to the container
                        string connectionString = hostContext.Configuration.GetConnectionString("LibraryDbConnection");

                        if (string.IsNullOrEmpty(connectionString))
                        {
                            throw new InvalidOperationException("The connection string 'LibraryDbConnection' is not configured.");
                        }

                        services.AddDbContext<LibraryDbContext>(options =>
                            options.UseSqlServer(connectionString));

                        // Add controllers
                        services.AddControllers();

                        // Add Swagger for API documentation
                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
                        });
                    });

                    webBuilder.Configure((context, app) =>
                    {
                        // Conditionally enable Swagger based on the environment
                        var env = context.HostingEnvironment;

                        //if (env.IsDevelopment())
                        //{
                            // Enable Swagger in the Development environment
                            app.UseSwagger();
                            app.UseSwaggerUI(c =>
                            {
                                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1");
                                c.RoutePrefix = string.Empty; // To access Swagger UI at the root (e.g., https://localhost:5001)
                            });
                        //}

                        // Enable CORS if needed (e.g., for local development)
                        app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                        // Use HTTPS redirection and routing
                        app.UseHttpsRedirection();
                        app.UseRouting();

                        // Enable authorization and authentication
                        app.UseAuthorization();

                        // Map controllers
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
