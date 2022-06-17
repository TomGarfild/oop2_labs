namespace Kernel.Data.Entities;

public record AlertData : BaseEntityData
{
    public AlertData()
    {
    }

    public AlertData(string tradingPair, decimal price, bool isLower, string userId)
    {
        TradingPair = tradingPair;
        Price = price;
        IsLower = isLower;
        UserId = userId;
    }

    public string TradingPair { get; init; }
    public decimal Price { get; init; }
    public bool IsLower { get; init; }
    public bool IsExecuted { get; init; }
    public virtual UserData User { get; init; }
    public string UserId { get; init; }
}