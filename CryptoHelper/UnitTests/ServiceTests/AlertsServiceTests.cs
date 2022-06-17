using Kernel.Data.Entities;
using Moq;
using UnitTests.EntitiesBuilders.Domain;

namespace UnitTests.ServiceTests;

public class AlertsServiceTests : UnitTestsBase
{
    [Test]
    public async Task CreateAlertTest()
    {
        // Arrange
        SetUpAlertDb();
        var alert = new InternalAlertBuilder().WithTradingPair("BTCUSDT").WithPrice(1000m).WithIsLower(false)
            .WithUserId(Guid.NewGuid().ToString()).Build();

        // Act
        await AlertsService.AddAsync(alert);

        // Assert
        MockAlertSet.Verify(u => u.AddAsync(It.IsAny<AlertData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}