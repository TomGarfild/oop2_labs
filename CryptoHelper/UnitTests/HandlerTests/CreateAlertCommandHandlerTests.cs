using FluentAssertions;
using Kernel.Data.Entities;
using Kernel.Requests.Commands;
using Moq;
using UnitTests.EntitiesBuilders.Domain;

namespace UnitTests.HandlerTests;

public class CreateAlertCommandHandlerTests : UnitTestsBase
{
    [Test]
    public async Task CreateAlertSuccessfully()
    {
        // Arrange
        SetUpAlertDb();
        SetUpUserDb();
        var user = new InternalUserBuilder().WithChatId(100)
            .WithUsername("admin")
            .WithFirstName("Pavel")
            .WithLastName("Durov").Build();

        // Act
        await UsersService.AddAsync(user);
        var res = await Mediator.Send(new CreateAlertCommand(100, "BTCUSDT", 1000m));

        // Assert
        res.Should().BeTrue();
        MockAlertSet.Verify(u => u.AddAsync(It.IsAny<AlertData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Exactly(2)); // One for user, another for alert
    }

    [Test]
    [Ignore("Should not depend on test above, but depends")]
    public async Task NotCreateAlertWithNotExistingUserSuccessfully()
    {
        // Arrange
        SetUpAlertDb();
        SetUpUserDb();

        // Act
        var res = await Mediator.Send(new CreateAlertCommand(0, "BTCUSDT", 1000m));

        // Assert
        res.Should().BeFalse();
        MockAlertSet.Verify(u => u.AddAsync(It.IsAny<AlertData>(), CancellationToken.None), Times.Never());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Never());
    }
}