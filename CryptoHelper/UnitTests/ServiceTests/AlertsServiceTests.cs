using Kernel.Data.Entities;
using Kernel.Domain.Entities;
using Moq;

namespace UnitTests.ServiceTests;

public class AlertsServiceTests : UnitTestsBase
{
    [Test]
    public async Task CreateAlert()
    {
        // Arrange
        MockAlertSet.Setup(m => m.AddAsync(It.IsAny<AlertData>(), CancellationToken.None))
            .Callback<AlertData, CancellationToken>((u, _) => { AlertSet.Add(u); });
        MockContext.Setup(x => x.Alerts).Returns(MockAlertSet.Object);
        var alert = new InternalAlert("BTCUSDT", 1000m, false, Guid.NewGuid().ToString());

        // Act
        await AlertsService.AddAsync(alert);

        // Assert
        MockAlertSet.Verify(u => u.AddAsync(It.IsAny<AlertData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}