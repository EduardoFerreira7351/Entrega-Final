using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FastPoint.Domain.Entities;

namespace FastPoint.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price).IsRequired().HasPrecision(18,2);
            builder.Property(x => x.IsActive).HasDefaultValue(true);

            // one-to-one product - stock (optional)
            builder.HasOne(p => p.Stock)
                   .WithOne(s => s.Product)
                   .HasForeignKey<Stock>(s => s.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
