namespace Kernel.Data.Entities;

public record AlertData : BaseEntityData
{
    public AlertData()
    {
    }

    public AlertData(string tradingPair, decimal price, bool isLower, bool isExecuted, bool isActive,
                     UserData user, string userId)
    {
        TradingPair = tradingPair;
        Price = price;
        IsLower = isLower;
        IsExecuted = isExecuted;
        IsActive = isActive;
        User = user;
        UserId = userId;
    }

    public string TradingPair { get; init; }
    public decimal Price { get; init; }
    public bool IsLower { get; init; }
    public bool IsExecuted { get; init; }
    public bool IsActive { get; init; }
    public UserData User { get; init; }
    public string UserId { get; init; }
}