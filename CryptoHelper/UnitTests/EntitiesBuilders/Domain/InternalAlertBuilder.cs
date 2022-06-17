using Kernel.Domain.Entities;

namespace UnitTests.EntitiesBuilders.Domain;

public class InternalAlertBuilder : IBuilder<InternalAlert>
{
    private string _tradingPair;
    private decimal _price;
    private bool _isLower;
    private string _userId;

    public InternalAlert Build()
    {
        return new InternalAlert(_tradingPair, _price, _isLower, _userId);
    }

    public InternalAlertBuilder WithTradingPair(string tradingPair)
    {
        _tradingPair = tradingPair;
        return this;
    }
    public InternalAlertBuilder WithPrice(decimal price)
    {
        _price = price;
        return this;
    }
    public InternalAlertBuilder WithIsLower(bool isLower)
    {
        _isLower = isLower;
        return this;
    }
    public InternalAlertBuilder WithUserId(string userId)
    {
        _userId = userId;
        return this;
    }
}