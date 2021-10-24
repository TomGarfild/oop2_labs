using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Blazor;

namespace TimerWebApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTI0MDk1QDMxMzkyZTMzMmUzMGdXWU85ay9XK0V2TmlKYXlVVkV5MzRSdDU3Q3AzK0JPamRSVXNvMFJCdlE9");

            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}
