using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunApp.Domain.CustomerProfileAggregate;
using RunApp.Domain.PhotoAggregate;
using RunApp.Domain.Products;

namespace RunApp.Infrastructure.Photos.Persistence
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasOne<Product>()
                .WithOne()
                .HasForeignKey<Photo>(x => x.ProductId);

            builder.HasOne<CustomerProfile>()
                .WithOne()
                .HasForeignKey<Photo>(x => x.CustomerProfileId);

            builder.HasKey(x => x.PhotoId);
        }
    }
}
