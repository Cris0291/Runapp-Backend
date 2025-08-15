using Microsoft.EntityFrameworkCore;
using RunApp.Domain.CustomerProfileAggregate;
using RunApp.Domain.Products;
using RunApp.Domain.ReviewAggregate;

namespace RunApp.Infrastructure.Reviews.Persistence
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.Date)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(r => r.ReviewDescription)
                 .HasConversion<ReviewEnumValueConverter>();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            builder.HasOne<CustomerProfile>()
                .WithMany()
                .HasForeignKey(x => x.Id);

            builder.HasKey(x => new {x.ProductId, x.Id});

            builder.HasAlternateKey(x => x.ReviewId);

            builder.HasIndex(x => x.ReviewId);
        }
    }
}
