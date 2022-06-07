using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kernel.Data;

public static class AppExtensions
{
    public static void AddKernelData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlDb")));
    }
}