using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Student_Dashboard_Api.Data;

namespace Student_Dashboard_Api.Services
{
    public static class DepencyInjection
    {
        public static IServiceCollection ApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<StudentService>();
            services.AddScoped<FileService>();
            raw.SetProvider(imp: new SQLite3Provider_e_sqlite3());
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("SQLiteConnection"));
            });
            Batteries.Init();

            return services;
        }
    }
}
