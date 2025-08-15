using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunApp.Domain.StoreOwnerProfileAggregate.Stocks;

namespace RunApp.Infrastructure.Stocks.Persistence
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasMany(x => x.Logs)
                .WithOne()
                .HasForeignKey(x => x.StockId);
        }
    }
}
