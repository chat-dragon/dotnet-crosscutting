using Microsoft.AspNetCore.Hosting;

namespace CrossCutting.DI.ProgramModules;

public static partial class ConfigurationModule
{
    public static void AddConfiguration(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, x) =>
         {
             //string? environmentName = context.HostingEnvironment.EnvironmentName;
             //x.AddJsonFile("appsettings.json", true, true);
             //x.AddJsonFile($"appsettings.{environmentName}.json", true);
             x.AddEnvironmentVariables();
         });
    }

    public static void AddOcelotConfiguration(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, x) =>
        {
            string? environmentName = context.HostingEnvironment.EnvironmentName;
            x.AddJsonFile("ocelot.json", true, true);
            x.AddJsonFile($"ocelot.{environmentName}.json", true);
            x.AddEnvironmentVariables();
        });
    }

    //public static void AddOcelotConfiguration(this IConfigurationBuilder configuration, IHostEnvironment environment)
    //{
    //    configuration.AddJsonFile("ocelot.json", true, true);
    //    configuration.AddJsonFile($"ocelot.{environment.EnvironmentName}.json", true);
    //    configuration.AddEnvironmentVariables();
    //}
}
