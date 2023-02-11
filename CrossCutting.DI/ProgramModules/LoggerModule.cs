using CrossCutting.DI.ProgramModules;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System.Diagnostics;

namespace CrossCutting.DI.ProgramModules;

public static partial class ProgramLoggerServices
{
    public static void AddSerilog(this IHostBuilder host, string applicationName)
    {
        host.UseSerilog((context, config) =>
        {
            if (context.HostingEnvironment.IsProduction())
                config.MinimumLevel.Information();
            else
            {
                config.MinimumLevel.Debug();
                config.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
            }
            config
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.WithProperty("AplicationName", applicationName)
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
            ;
        });
    }
}
