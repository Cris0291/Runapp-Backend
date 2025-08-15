using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunApp.Domain.CustomerProfileAggregate;
using RunApp.Domain.OrderAggregate;
using RunApp.Domain.UserAggregate;

namespace RunApp.Infrastructure.CustomerProfiles.Persistence
{
    public class CustomerProfileConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<AppUser>()
                .WithOne()
                .HasForeignKey<CustomerProfile>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Order>()
                .WithOne()
                .HasForeignKey(x => x.Id);

            builder.OwnsOne(x => x.ShippingAdress, px => px.Property(y => y.ZipCode).IsRequired(false));
            builder.OwnsOne(x => x.ShippingAdress, px => px.Property(y => y.Street).IsRequired(false));
            builder.OwnsOne(x => x.ShippingAdress, px => px.Property(y => y.City).IsRequired(false));
            builder.OwnsOne(x => x.ShippingAdress, px => px.Property(y => y.Country).IsRequired(false));

            builder.OwnsOne(x => x.PaymentMethod);
        }
    }
}
