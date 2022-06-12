using Kernel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kernel.Data;

public class DataDbContext : DbContext
{
    public DataDbContext()
    {
    }

    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    public virtual DbSet<UserData> Users { get; set; }
    public virtual DbSet<AlertData> Alerts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlertData>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);
    }
}