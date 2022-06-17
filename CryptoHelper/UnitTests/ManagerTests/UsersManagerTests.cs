using FluentAssertions;
using Kernel.Common.ActionTypes;
using Kernel.Data.Entities;
using Moq;
using UnitTests.EntitiesBuilders.Data;

namespace UnitTests.ManagerTests;

public class UsersManagerTests : UnitTestsBase
{
    [Test]
    public async Task CreateUserTest()
    {
        // Arrange
        SetUpUserDb();
        var user = new UserDataBuilder().WithChatId(100)
            .WithUsername("admin")
            .WithFirstName("Pavel")
            .WithLastName("Durov").Build();

        // Act
        await UsersManager.UpdateAsync(user, UserActionType.Created, CancellationToken.None);

        // Assert
        MockUserSet.Verify(u => u.AddAsync(It.IsAny<UserData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
        UserSet.Should().OnlyContain(u => u.ChatId == user.ChatId);
    }
}