using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Moq;
using UnitTests.EntitiesBuilders.Data;

namespace UnitTests.ManagerTests;

public class AlertsManagerTests : UnitTestsBase
{
    [Test]
    public async Task CreateAlertTest()
    {
        // Arrange
        SetUpAlertDb();
        var alert = new AlertDataBuilder().WithTradingPair("BTCUSDT").WithPrice(1000m).WithIsLower(false)
            .WithUserId(Guid.NewGuid().ToString()).Build();

        // Act
        await AlertsManager.UpdateAsync(alert, AlertActionType.Created, CancellationToken.None);

        // Assert
        MockAlertSet.Verify(u => u.AddAsync(It.IsAny<AlertData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}