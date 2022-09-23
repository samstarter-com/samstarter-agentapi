using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SWI.SoftStock.AgentWebApi.IIS2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().ConfigureLogging((hostingContext, logging) =>
                    {
                        // The ILoggingBuilder minimum level determines the
                        // the lowest possible level for logging. The log4net
                        // level then sets the level that we actually log at.                       
                        logging.SetMinimumLevel(LogLevel.Debug);
                    });
                });
    }
}
