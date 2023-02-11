using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MassTransit;
using CrossCutting.DI.ProgramModules;

namespace CrossCutting.DI.ProgramModules;

public static partial class ProgramMensageriaServices
{
    public static IServiceCollection AddMediatorWithConfigure(this IServiceCollection services, Action<IMediatorRegistrationContext, IMediatorConfigurator>? configure, params Assembly[] assemblies)
    {
        services.AddMediator(cfg =>
        {
            cfg.AddConsumers(assemblies); // Request Handlers
            cfg.ConfigureMediator(configure ?? ((c, x) => { }));
        });
        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
    {
        return AddMediatorWithConfigure(services, null, assemblies);
    }
}
