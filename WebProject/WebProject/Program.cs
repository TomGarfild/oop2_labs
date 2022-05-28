using Kernel;
using Serilog;
using TelegramBot;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, lc) => lc.WriteTo.Console());

builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<BinanceApiOptions>(builder.Configuration.GetSection("BinanceApiSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKernel();
builder.Services.AddHandlers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseKernel();
app.UseHandlers();
app.UseAuthorization();

app.MapControllers();

app.Run();
