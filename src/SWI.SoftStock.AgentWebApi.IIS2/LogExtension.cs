using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;

namespace SWI.SoftStock.AgentWebApi.IIS2
{
    public static class LogExtension
    {
        /// <summary>
        /// Adds customized logging <paramref name="services"/> via Serilog.
        /// 
        /// When deployed to AWS ECS, logs will go to stdout and will be automatically transferred to CloudWatch.
        /// </summary>
        public static IServiceCollection AddLoggingCustomized(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration.GetSection("Logging"))
                .WriteTo.Console(new CompactJsonFormatter())
                .CreateLogger();

            services.AddLogging(builder =>
                builder.AddSerilog()
            );

            return services;
        }
    }
}
