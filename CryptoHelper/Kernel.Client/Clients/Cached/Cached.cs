namespace Kernel.Client.Clients.Cached;

public record Cached<T>(T Value, DateTime ExpirationDateTime);