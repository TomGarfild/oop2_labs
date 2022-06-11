namespace Kernel.Domain.Entities;

public sealed record InternalUser(string Id, long ChatId, string Username, string FirstName, string LastName) : IHasId;