using System.Reflection;
using Mediator.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mediator;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, ServiceLifetime lifetime, params Assembly[] assemblies)
    {
        var handlerInfo = new Dictionary<Type, Type>();

        foreach (var assembly in assemblies)
        {
            var requests = GetClassesImplementingInterface(assembly, typeof(IRequest<>));
            var handlers = GetClassesImplementingInterface(assembly, typeof(IRequestHandler<,>));

            foreach (var request in requests)
            {
                handlerInfo[request] = handlers.SingleOrDefault(h => request == h.GetInterface(typeof(IRequestHandler<,>).Name)!.GetGenericArguments()[0]);
            }

            var serviceDescriptors = handlers.Select(h => new ServiceDescriptor(h, h, lifetime));
            services.TryAdd(serviceDescriptors);
        }

        services.AddSingleton<IMediator>(m => new MediatorImpl(m.GetRequiredService, handlerInfo));

        return services;
    }

    private static List<Type> GetClassesImplementingInterface(Assembly assembly, Type typeToMatch)
    {
        return assembly.ExportedTypes.Where(type => !type.IsInterface && !type.IsAbstract &&
                                                    type.GetInterfaces().Where(t => t.IsGenericType)
                                                        .Any(t => t.GetGenericTypeDefinition() == typeToMatch)).ToList();
    }
}