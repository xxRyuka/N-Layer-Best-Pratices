using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N_LayerBestPratice.Repository.Stores;

namespace N_LayerBestPratice.Repository.Stores;

public class CategoryConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.StoreName)
            .IsRequired()
            .HasMaxLength(100);

    }
}