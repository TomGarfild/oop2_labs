namespace Kernel.Domain.Entities;

public record InternalAlert(string TradingPair, decimal Price, bool IsLower);