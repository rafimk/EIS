using EIS.Shared.Contexts.Accessors;
using EIS.Shared.Contexts.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace EIS.Shared.Contexts;

public static class Extensions
{
    public static IServiceCollection AddContexts(this IServiceCollection services)
    {
        services.AddSingleton<IContextProvider, ContextProvider>();
        services.AddSingleton<IContextAccessor, ContextAccessor>();

        return services;
    }
}