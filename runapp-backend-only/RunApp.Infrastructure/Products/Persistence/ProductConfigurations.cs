using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunApp.Domain.Products;
using RunApp.Domain.StoreOwnerProfileAggregate.Sales;
using RunApp.Domain.StoreOwnerProfileAggregate.Stocks;

namespace RunApp.Infrastructure.Products.Persistence
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.OwnsMany(product => product.BulletPoints)
                .ToTable("Bulletpoints");

            builder.OwnsOne(p => p.PriceOffer, pO => pO.Property(x => x.Discount).HasComputedColumnSql("100 * (1-[PriceWithDiscount]/[ActualPrice])", stored: true)
                 .HasColumnType("decimal(5,2)").HasColumnName("Discount"));

            builder.OwnsOne(p => p.PriceOffer, px => px.Property(x => x.PriceWithDiscount).HasColumnType("decimal(10,2)").HasColumnName("PriceWithDiscount"));
            builder.OwnsOne(p => p.PriceOffer, px => px.Property(x => x.PromotionalText).HasColumnName("PromotionalText"));

            builder.OwnsOne(p => p.Characteristic, px => px.Property(x => x.Weight).HasColumnType("decimal(6,2)"));

            builder.Property(p => p.ActualPrice)
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.AverageRatings)
                .HasColumnType("decimal(4,2)");

            builder.HasMany<Sale>()
                .WithOne(x => x.ProductSold)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne<Stock>()
                .WithOne()
                .HasForeignKey<Stock>(x => x.StockProductId);

            builder.HasMany(x => x.Categories)
                .WithMany();

            builder.Property("_averageSum")
                .HasColumnName("AverageSum");


            /* builder.HasData(new Product()
             {
                 ProductId = Guid.NewGuid(),
                 Name = "Xbox",
                 PriceWithDiscount = 275.75m,
                 ActualPrice = 500m,
                 Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                 PromotionalText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse vitae enim dolor. Nunc in nibh lectus. Cvestibulum id augue. Sed luctus convar. Sed interdum non quam quis eleifend",
             },
             new Product()
             {
                 ProductId = Guid.NewGuid(),
                 Name = "PlayStation",
                 ActualPrice = 500m,
                 Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                 PromotionalText = "",
             }
             );*/
        }
    }
}
