using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kernel;

public static class AppExtensions
{
    public static void AddKernel(this IServiceCollection services)
    {
        services.AddSingleton<Client>();
    }

    public static void UseKernel(this IApplicationBuilder app)
    {
            
    }
}