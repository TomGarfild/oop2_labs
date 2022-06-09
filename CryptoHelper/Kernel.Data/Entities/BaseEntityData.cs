namespace Kernel.Data.Entities;

public record BaseEntityData
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DateTime TimeStamp { get; init; } = DateTime.Now;
    public int DataVersion { get; init; } = 1;
};