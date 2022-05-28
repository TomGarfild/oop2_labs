using Kernel;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, lc) => lc.WriteTo.Console());

builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<BinanceApiOptions>(builder.Configuration.GetSection("BinanceApiSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKernel();

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
