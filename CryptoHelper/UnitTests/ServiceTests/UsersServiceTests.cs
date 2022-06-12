using FluentAssertions;
using Kernel.Data.Entities;
using Kernel.Domain.Entities;
using Moq;

namespace UnitTests.ServiceTests;

public class UsersServiceTests : UnitTestsBase
{
    [Test]
    public async Task CreateUser()
    {
        // Arrange
        MockUserSet.Setup(m => m.AddAsync(It.IsAny<UserData>(), CancellationToken.None))
            .Callback<UserData, CancellationToken>((u, _) => { UserSet.Add(u); });
        MockContext.Setup(x => x.Users).Returns(MockUserSet.Object);
        var user = new InternalUser(100, "admin", "Pavel", "Durov");

        // Act
        await UsersService.AddAsync(user);

        // Assert
        MockUserSet.Verify(u => u.AddAsync(It.IsAny<UserData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
        UserSet.Should().OnlyContain(u => u.ChatId == user.ChatId);
    }
}