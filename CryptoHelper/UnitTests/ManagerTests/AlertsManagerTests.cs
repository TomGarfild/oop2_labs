using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Moq;

namespace UnitTests.ManagerTests;

public class AlertsManagerTests : UnitTestsBase
{
    [Test]
    public async Task CreateAlertTest()
    {
        // Arrange
        MockAlertSet.Setup(m => m.AddAsync(It.IsAny<AlertData>(), CancellationToken.None))
            .Callback<AlertData, CancellationToken>((u, _) => { AlertSet.Add(u); });
        MockContext.Setup(x => x.Alerts).Returns(MockAlertSet.Object);
        var alert = new AlertData("BTCUSDT", 1000m, false, Guid.NewGuid().ToString());

        // Act
        await AlertsManager.UpdateAsync(alert, AlertActionType.Created, CancellationToken.None);

        // Assert
        MockAlertSet.Verify(u => u.AddAsync(It.IsAny<AlertData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}