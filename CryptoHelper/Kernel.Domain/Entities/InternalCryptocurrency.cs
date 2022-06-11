namespace Kernel.Domain.Entities;

public sealed record InternalCryptocurrency(string Id, string Name, string Symbol, string Url) : IHasId;