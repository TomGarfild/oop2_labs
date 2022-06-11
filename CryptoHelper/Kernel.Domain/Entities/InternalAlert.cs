namespace Kernel.Domain.Entities;

public sealed record InternalAlert(string Id, string TradingPair, decimal Price, bool IsLower) : IHasId;