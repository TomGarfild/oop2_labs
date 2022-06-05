using Kernel;
using Kernel.Client.Options;
using Serilog;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Host.UseSerilog((_, lc) => lc.WriteTo.Console());

var telegramSettingsSection = configuration.GetSection("TelegramSettings");
var token = telegramSettingsSection.Get<TelegramOptions>().ApiToken;
builder.Services.Configure<TelegramOptions>(telegramSettingsSection);
builder.Services.Configure<Dictionary<string, ApiOptions>>(configuration.GetSection("ApiSettings"));
builder.Services.Configure<BinanceApiOptions>(configuration.GetSection("BinanceApiSettings"));

builder.Services.AddHttpClient("TelegramBot")
    .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(token, httpClient));
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

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("TelegramBot", $"api/{token}",
        new { controller = "TelegramBot", action = "Update" });
    endpoints.MapControllers();
});

app.Run();
