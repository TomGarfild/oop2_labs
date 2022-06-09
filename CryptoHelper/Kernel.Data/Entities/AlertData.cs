namespace Kernel.Data.Entities;

public class AlertData : BaseEntityData
{
    public string TradingPair { get; set; }
    public decimal Price { get; set; }
    public bool IsLower { get; set; }
    public bool IsExecuted { get; set; }
    public bool IsActive { get; set; }

    public UserData User { get; set; }
    public string UserId { get; set; }
}