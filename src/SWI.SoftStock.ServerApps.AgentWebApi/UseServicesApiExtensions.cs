using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWI.SoftStock.ServerApps.AgentServices;
using SWI.SoftStock.ServerApps.DataModel2;

namespace SWI.SoftStock.ServerApps.AgentWebApi
{
    public static class UseServicesApiExtensions
    {
        public static IServiceCollection AddServicesApi(this IServiceCollection services, IConfiguration configuration)
        {
            var dbOption = (new DbContextOptionsBuilder<MainDbContext>()).UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            services.AddSingleton(b => dbOption.Options);

            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<MainDbContextFactory>();

            services.AddScoped<CheckCompanyService>();
            services.AddScoped<MachineService>();
            services.AddScoped<OperationSystemService>();
            services.AddScoped<ProcessService>();
            services.AddScoped<SoftwareService>();
            services.AddScoped<UserService>();

            return services;
        }


    }
}
