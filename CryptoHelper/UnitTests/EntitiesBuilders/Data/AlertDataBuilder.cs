using Kernel.Data.Entities;

namespace UnitTests.EntitiesBuilders.Data;

public class AlertDataBuilder : IBuilder<AlertData>
{
    private string _tradingPair;
    private decimal _price;
    private bool _isLower;
    private string _userId;

    public AlertData Build()
    {
        return new AlertData(_tradingPair, _price, _isLower, _userId);
    }

    public AlertDataBuilder WithTradingPair(string tradingPair)
    {
        _tradingPair = tradingPair;
        return this;
    }
    public AlertDataBuilder WithPrice(decimal price)
    {
        _price = price;
        return this;
    }
    public AlertDataBuilder WithIsLower(bool isLower)
    {
        _isLower = isLower;
        return this;
    }
    public AlertDataBuilder WithUserId(string userId)
    {
        _userId = userId;
        return this;
    }
}