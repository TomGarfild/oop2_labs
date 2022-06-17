namespace Kernel.Data.Entities;

public record UserData : BaseEntityData
{
    public UserData()
    {
    }

    public UserData(long chatId) : this(chatId, null, null, null)
    {
    }

    public UserData(long chatId, string username, string firstName, string lastName)
    {
        ChatId = chatId;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }

    public long ChatId { get; init; }
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}