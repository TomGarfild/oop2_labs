using Kernel;
using Kernel.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Host.UseSerilog((_, lc) => lc.WriteTo.Console());

var telegramSettingsSection = configuration.GetSection("TelegramSettings");
builder.Services.Configure<TelegramOptions>(telegramSettingsSection);
builder.Services.Configure<ApiOptions>(configuration.GetSection("ApiSettings"));
builder.Services.Configure<BinanceApiOptions>(configuration.GetSection("BinanceApiSettings"));

builder.Services.AddKernel(configuration);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseKernel();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    var token = telegramSettingsSection.Get<TelegramOptions>().ApiToken;
    endpoints.MapControllerRoute("TelegramBot",$"bot/{token}",
        new { controller = "TelegramBot", action = "Update" });
    endpoints.MapControllers();
});

app.Run();
