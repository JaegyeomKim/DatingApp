using DatingAppAPI.Data;
using DatingAppAPI.Helpers;
using DatingAppAPI.Interfaces;
using DatingAppAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace DatingAppAPI.Extenstions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services,
            IConfiguration config)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true";

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            // services.AddAutoMapper() : Dependency Injection the AutoMapper in the container
            // AppDomain.CurrentDomain.GetAssemblies() : 
            // AppDomain : Includ information about app env when it runs. 
            // CurrentDomain: It represents the AppDomain:code running env of the currently running app.
            // GetAssemblies(): Return all assemblies loaded in the current AppDomain.
            // an assembly is the unit of execution in .Net, typically consisting of compiled code in DLL or EXE files.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            return services;
        }
    }
}
