using FluentAssertions;
using Kernel.Data.Entities;
using Moq;
using UnitTests.EntitiesBuilders.Domain;

namespace UnitTests.ServiceTests;

public class UsersServiceTests : UnitTestsBase
{
    [Test]
    public async Task CreateUserTest()
    {
        // Arrange
        SetUpUserDb();
        var user = new InternalUserBuilder().WithChatId(100)
                                            .WithUsername("admin")
                                            .WithFirstName("Pavel")
                                            .WithLastName("Durov").Build();

        // Act
        await UsersService.AddAsync(user);

        // Assert
        MockUserSet.Verify(u => u.AddAsync(It.IsAny<UserData>(), CancellationToken.None), Times.Once());
        MockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once());
        UserSet.Should().OnlyContain(u => u.ChatId == user.ChatId);
    }
}