namespace Kernel.Data.Entities;

public record BaseEntityData : IEntityData<string>
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public bool IsActive { get; init; } = true;
    public DateTime TimeStamp { get; init; } = DateTime.Now;
    public int DataVersion { get; init; } = 1;
};