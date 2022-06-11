namespace Kernel.Domain.Entities;

public sealed record InternalUser(long ChatId, string Username, string FirstName, string LastName) : IHasId
{
    public string Id { get; init; }
}