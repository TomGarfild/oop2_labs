using Kernel.Clients;
using Microsoft.Extensions.Logging;

namespace Kernel;

public class TimerHandler : IDisposable
{
    private Timer _timer;
    private readonly MarketClient _marketClient;
    private readonly ILogger _logger;

    public TimerHandler(MarketClient client, ILoggerFactory loggerFactory)
    {
        _marketClient = client;
        _logger = loggerFactory.CreateLogger<TimerHandler>();
    }

    public EventHandler reachedPrice;

    public void Start(double stopPrice)
    {
        _timer?.Change(Timeout.Infinite, Timeout.Infinite);
        _timer = new Timer(
            async _ =>
            {
                var res = await _marketClient.GetSymbolPriceTicker("BTCUSDT");
                _logger.LogInformation($"{res.Symbol}: {res.Price}");
                if (res.Price <= stopPrice)
                {
                    reachedPrice?.Invoke(this, EventArgs.Empty);
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                }
            },
            null,
            0,
            1000);
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}