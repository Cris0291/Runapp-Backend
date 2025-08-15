using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunApp.Domain.OrderAggregate;

namespace RunApp.Infrastructure.Orders.Persistence
{
    internal class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.TotalPrice)
                .HasColumnType("decimal(20,2)");

            builder.OwnsOne(x => x.Address);
            builder.OwnsOne(x => x.PaymentMethod);

            builder.Property(x => x.DateOfPayment)
                .HasDefaultValueSql("getutcdate()");

            builder.HasKey(x => x.OrderId);
        }
    }
}
