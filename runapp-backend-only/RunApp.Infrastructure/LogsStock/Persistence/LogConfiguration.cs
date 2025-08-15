using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunApp.Domain.StoreOwnerProfileAggregate.Stocks.LogsStock;

namespace RunApp.Infrastructure.LogsStock.Persistence
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.Property(x => x.StockAddedDate)
                .HasComputedColumnSql(@"case when [AddedStock] is not null then [StockDate]
                                              else null end", stored: true);

            builder.Property(x => x.StockRemoveDate)
                .HasComputedColumnSql(@"case when [RemovedStock] is not null then [StockDate]
                                              else null end", stored: true);
            builder.Property(x => x.StockSoldDate)
                .HasComputedColumnSql(@"case when [SoldStock] is not null then [StockDate]
                                              else null end", stored: true);

            builder.Property(x => x.StockDate)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
