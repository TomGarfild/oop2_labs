using FluentAssertions;
using Kernel.Data.Entities;
using Kernel.Requests.Commands;
using Moq;

namespace UnitTests.HandlerTests;

public class CreateUserCommandHandlerTests : UnitTestsBase
{
    [Test]
    public async Task CreateAlertSuccessfully()
    {
        // Arrange
        SetUpUserDb();

        // Act
        await Mediator.Send(new CreateUserCommand(100, "admin", "Pavel", "Durov"));

        // Assert
        MockUserSet.Verify(u => u.AddAsync(It.IsAny<UserData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
        UserSet.Should().OnlyContain(u => u.ChatId == 100);
    }
}