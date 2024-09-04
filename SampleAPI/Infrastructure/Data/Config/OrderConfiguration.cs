using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleAPI.Entities;

namespace SampleAPI.Infrastructure.Data.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // builder.ToTable("Order");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(100);

        builder.Property(x => x.IsInvoiced)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired();
        
    }
}