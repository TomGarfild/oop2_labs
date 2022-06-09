namespace Kernel.Data.Entities;

public record UserData : BaseEntityData
{
    public UserData()
    {
    }

    public UserData(long chatId, string username, string firstName, string lastName, bool isActive)
    {
        ChatId = chatId;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        IsActive = isActive;
    }

    public long ChatId { get; init; }
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public bool IsActive { get; init; }
}