using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kernel.Data.Managers;

namespace Kernel.Data;

public static class AppExtensions
{
    public static void AddKernelData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlDb")), ServiceLifetime.Singleton);
        services.AddSingleton<UsersManager>();
        services.AddSingleton<AlertsManager>();
    }
}