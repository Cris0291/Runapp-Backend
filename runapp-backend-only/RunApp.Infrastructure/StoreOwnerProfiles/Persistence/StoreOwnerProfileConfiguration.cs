using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunApp.Domain.StoreOwnerProfileAggregate;
using RunApp.Domain.UserAggregate;

namespace RunApp.Infrastructure.StoreOwnerProfiles.Persistence
{
    public class StoreOwnerProfileConfiguration : IEntityTypeConfiguration<StoreOwnerProfile>
    {
        public void Configure(EntityTypeBuilder<StoreOwnerProfile> builder)
        {
            builder.HasMany(x => x.Sales)
                 .WithOne()
                 .HasForeignKey(y => y.StoreOwnerProfileId);

            builder.HasMany(x => x.Stocks)
                .WithOne()
                .HasForeignKey(y => y.StoreOwnerProfileId);

            builder.Property(x => x.TotalSalesInCash).HasPrecision(10, 2);

            builder.OwnsOne(x => x.BussinesAdress);

            builder.OwnsOne(x => x.CreditOrBussinesCard);

            builder.HasOne<AppUser>()
                .WithOne()
                .HasForeignKey<StoreOwnerProfile>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.SalesLevel)
                .HasConversion<SalesLevelConverter>();

            builder.Property(x => x.SalesLevel)
                .HasComputedColumnSql(@"case when [TotalProductsSold] >=0 and [TotalProductsSold] < 1000 then 'Junior' 
                                        when [TotalProductsSold] >= 1000 and [TotalProductsSold] < 5000 then 'Intermediate'
                                         else 'Senior' end", stored: true);

            builder.Property(x => x.InitialInvestment).HasPrecision(10, 2);

            builder.HasKey(x => x.StoreProfileId);
        }
    }
}
