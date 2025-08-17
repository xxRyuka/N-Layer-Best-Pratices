using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N_LayerBestPratice.Repository.Products;

namespace N_LayerBestPratice.Repository.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Stock)
            .IsRequired();


        builder.HasOne(p => p.Store)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.StoreId);
        // Additional configurations can be added here
        // For example, you can configure indexes, relationships, etc.
    }
}