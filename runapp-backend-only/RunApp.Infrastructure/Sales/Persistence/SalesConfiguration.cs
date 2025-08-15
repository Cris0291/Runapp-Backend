using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunApp.Domain.StoreOwnerProfileAggregate.Sales;

namespace RunApp.Infrastructure.Sales.Persistence
{
    public class SalesConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(x => x.AmountSold).HasPrecision(10, 2);

            builder.Property(x => x.DateOfTheSale)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
