using Kernel.Clients;
using Microsoft.Extensions.Logging;

namespace Kernel.Builders;

public class TimerBuilder
{
    private Timer _timer;
    private readonly MarketClient _marketClient;
    private readonly ILogger _logger;

    public TimerBuilder(MarketClient client, ILoggerFactory loggerFactory)
    {
        _marketClient = client;
        _logger = loggerFactory.CreateLogger<TimerBuilder>();
    }

    public EventHandler reachedPrice;

    public void Start(string symbol, double stopPrice)
    {
        _timer = new Timer(
            async _ =>
            {
                /*var res = await _marketClient.GetSymbolPriceTicker(symbol);
                var sign = stopPrice < 0 ? "below" : "higher";
                _logger.LogInformation($"{res.Symbol}: {res.Price} (StopPrice: {sign} {Math.Abs(stopPrice)})");
                if (stopPrice < 0 && res.Price <= -stopPrice || stopPrice >= 0 && res.Price >= stopPrice)
                {
                    reachedPrice?.Invoke(this, EventArgs.Empty);
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                }*/
            },
            null,
            0,
            1000);
    }

    public Timer Create(Func<object, Task> action, int delay, int period, params object[] args)
    {
        return new Timer(async v => await action(v), args, delay, period);
    }
}