namespace Kernel.Data.Entities;

public class BaseEntityData
{
    public string Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public int DataVersion { get; set; }
}