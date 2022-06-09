using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    public DbSet<UserData> Users { get; set; }
    public DbSet<AlertData> Alerts { get; set; }
}