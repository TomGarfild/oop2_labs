using Mediator;

namespace Kernel.Requests.Commands;

// TODO: add requests with void or some Result type
public record CreateAlertCommand(long ChatId, string TradingPair, decimal Price) : IRequest<bool>;