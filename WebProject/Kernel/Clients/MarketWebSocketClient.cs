using Binance.Common;
using Binance.Spot;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kernel.Clients;

public class MarketWebSocketClient
{
    private readonly ILogger _logger;
    private readonly MarketDataWebSocket _marketWebSocket;

    public MarketWebSocketClient(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<MarketWebSocketClient>();
        _marketWebSocket = new MarketDataWebSocket("btcusdt@ticker");
    }

    private async Task M()
    {
        var onlyOneMessage = new TaskCompletionSource<string>();

        _marketWebSocket.OnMessageReceived(
            async (data) =>
            {
                onlyOneMessage.SetResult(data);
            }, CancellationToken.None);

        await _marketWebSocket.ConnectAsync(CancellationToken.None);

        var message = await onlyOneMessage.Task;

        _logger.LogInformation(message);

        await _marketWebSocket.DisconnectAsync(CancellationToken.None);
    }
}