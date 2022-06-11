namespace Kernel.Domain.Entities;

public sealed record InternalAlert(string TradingPair, decimal Price, bool IsLower, string UserId) : IHasId
{
    public string Id { get; init; }
}