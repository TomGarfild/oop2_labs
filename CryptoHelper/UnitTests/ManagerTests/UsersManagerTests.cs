using FluentAssertions;
using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Kernel.Domain.Entities;
using Moq;

namespace UnitTests.ManagerTests;

public class UsersManagerTests : UnitTestsBase
{
    [Test]
    public async Task CreateUserTest()
    {
        // Arrange
        MockUserSet.Setup(m => m.AddAsync(It.IsAny<UserData>(), CancellationToken.None))
            .Callback<UserData, CancellationToken>((u, _) => { UserSet.Add(u); });
        MockContext.Setup(x => x.Users).Returns(MockUserSet.Object);
        var user = new UserData(100, "admin", "Pavel", "Durov");

        // Act
        await UsersManager.UpdateAsync(user, UserActionType.Created, CancellationToken.None);

        // Assert
        MockUserSet.Verify(u => u.AddAsync(It.IsAny<UserData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
        UserSet.Should().OnlyContain(u => u.ChatId == user.ChatId);
    }
}