using Kernel.Data.Entities;

namespace UnitTests.EntitiesBuilders.Data;

public class UserDataBuilder : IBuilder<UserData>
{
    private long _chatId;
    private string _username;
    private string _firstName;
    private string _lastName;

    public UserData Build()
    {
        return new UserData(_chatId, _username, _firstName, _lastName);
    }

    public UserDataBuilder WithChatId(long chatId)
    {
        _chatId = chatId;
        return this;
    }
    public UserDataBuilder WithUsername(string username)
    {
        _username = username;
        return this;
    }
    public UserDataBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }
    public UserDataBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }
}