namespace UiMetadataFramework.Web
{
    using System.IO;
    using Lamar.Microsoft.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseLamar()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                    webBuilder.ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddDebug();
                    });

                    webBuilder.ConfigureServices(services =>
                    {
                        // This is important, the call to AddControllers()
                        // cannot be made before the usage of ConfigureWebHostDefaults
                        services.AddControllers();
                        services.AddApplicationInsightsTelemetry();
                    });
                });

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}