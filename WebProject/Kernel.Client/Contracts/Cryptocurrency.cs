using Newtonsoft.Json;

namespace Kernel.Client.Contracts;

public record Cryptocurrency
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Symbol { get; init; }
    [JsonProperty("last_updated")]
    public string LastUpdated { get; init; }
}