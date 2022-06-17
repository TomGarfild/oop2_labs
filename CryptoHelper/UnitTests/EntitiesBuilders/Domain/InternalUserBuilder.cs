using Kernel.Domain.Entities;

namespace UnitTests.EntitiesBuilders.Domain;

public class InternalUserBuilder : IBuilder<InternalUser>
{
    private long _chatId;
    private string _username;
    private string _firstName;
    private string _lastName;
    
    public InternalUser Build()
    {
        return new InternalUser(_chatId, _username, _firstName, _lastName);
    }

    public InternalUserBuilder WithChatId(long chatId)
    {
        _chatId = chatId;
        return this;
    }
    public InternalUserBuilder WithUsername(string username)
    {
        _username = username;
        return this;
    }
    public InternalUserBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }
    public InternalUserBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }
}