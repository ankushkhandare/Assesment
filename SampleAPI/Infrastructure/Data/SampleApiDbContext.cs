using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;

namespace SampleAPI.Infrastructure.Data;
public class SampleApiDbContext : DbContext
{
    public SampleApiDbContext() { }
    public SampleApiDbContext(DbContextOptions<SampleApiDbContext> options) :
        base(options)
    {
    }

    public virtual DbSet<Order> Order { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasKey(o => o.Id);
        modelBuilder.Entity<Order>().Property(o => o.Description).HasMaxLength(100);
        modelBuilder.Entity<Order>().Property(o => o.Name).HasMaxLength(100);
        modelBuilder.Entity<Order>().Property(o => o.EntryDate);
        modelBuilder.Entity<Order>().Property(o => o.IsInvoiced);
        modelBuilder.Entity<Order>().Property(o => o.IsDeleted);
    }
    
}