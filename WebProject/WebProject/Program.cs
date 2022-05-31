using Kernel;
using Kernel.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Host.UseSerilog((_, lc) => lc.WriteTo.Console());

builder.Services.Configure<TelegramOptions>(configuration.GetSection("TelegramSettings"));
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

app.MapControllers();

app.Run();
