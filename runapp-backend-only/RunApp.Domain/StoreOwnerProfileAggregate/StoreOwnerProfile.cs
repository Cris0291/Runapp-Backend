using ErrorOr;
using RunApp.Domain.Common;
using RunApp.Domain.Products;
using RunApp.Domain.StoreOwnerProfileAggregate.Sales;
using RunApp.Domain.StoreOwnerProfileAggregate.Stocks;
using RunApp.Domain.StoreOwnerProfileAggregate.Stocks.LogsStock;
using RunApp.Domain.StoreOwnerProfileAggregate.StoreOwnerProfileErrors;
using RunApp.Domain.StoreOwnerProfileAggregate.ValueTypes;

namespace RunApp.Domain.StoreOwnerProfileAggregate
{
    public class StoreOwnerProfile : ValidationHandler
    {
        private StoreOwnerProfile() { }
        public Guid StoreProfileId { get; private set; }
        public string StoreName { get; private set; }
        public int TotalProductsSold { get; private set; }
        public decimal TotalSalesInCash { get; private set; }
        public int TotalStock { get; private set; }
        public int TotalRemovedStock { get; private set; }
        public bool IsAccountPaid { get; private set; }
        public decimal InitialInvestment { get; private set; }
        public Address BussinesAdress { get; private set; }
        public Card CreditOrBussinesCard { get; private set; }
        public SalesLevel SalesLevel { get; private set; }
        public  List<Sale> Sales { get; private set; }
        public List<Stock> Stocks { get; private set; }
        public Guid Id { get; private set; }

        public static ErrorOr<StoreOwnerProfile> CreateProfile(Guid userId, string storeName, int zipCode, string street,
            string city, int buildingNumber, string country,
            string? alternativeStreet, int? alternativeBuildingNumber,
            string holdersName, int cardNumber, int cvv, DateTime expiryDate, decimal initialInvestment)
        {
            AddValidation(nameof(StoreOwnerprofileError.InitialInvestmentCannotBeLowerThan5000), ()=> initialInvestment <= 5000);
            Validate();
            if (HasError()) return Errors;

            var address = new Address()
            {
                ZipCode = zipCode,
                Street = street,
                City = city,
                BuildingNumber = buildingNumber,
                Country = country,
                AlternativeStreet = alternativeStreet,
                AlternativeBuildingNumber = alternativeBuildingNumber
            };

            var card = new Card()
            {
                HoldersName = holdersName,
                CardNumber = cardNumber,
                CVV = cvv,
                ExpityDate = expiryDate
            };

            return new StoreOwnerProfile()
            {
                StoreName = storeName,
                Id = userId,
                BussinesAdress = address,
                CreditOrBussinesCard = card,
                TotalProductsSold = 0,
                TotalSalesInCash = 0,
                TotalStock = 0,
                IsAccountPaid = false,
                InitialInvestment = initialInvestment
            };
        }
        public ErrorOr<Success> CreateStock(Product product)
        {
            var stock =  new Stock()
            {
                ProductName = product.Name,
                TotalQuantity = 0,
                SoldQuantity = 0,
                Brand = product.Characteristic.Brand,
                ProductType = product.Characteristic.Type,
                StockProductId = product.ProductId
            };

            Stocks.Add(stock);
            return Result.Success;
        }
        public ErrorOr<Stock> AddStock(int addedStock, Guid stockProductId)
        {
            var errorOrStock = FindStock(stockProductId);
            if (errorOrStock.IsError) return errorOrStock.Errors;

            var stock = errorOrStock.Value;

            stock.TotalQuantity += addedStock;
            TotalStock += addedStock;

            var log = CreateLog(nameof(Log.AddedStock), addedStock);

            stock.Logs.Add(log);

            
            return stock;
        }

        public ErrorOr<Success> RemoveStock(int removedStock, Guid stockProductId)
        {
            var errorOrStock = FindStock(stockProductId);
            if (errorOrStock.IsError) return errorOrStock.Errors;

            var stock = errorOrStock.Value;

            stock.RemovedQuantity += removedStock;
            stock.TotalQuantity -= removedStock;

            TotalStock -= removedStock;
            TotalRemovedStock += removedStock;

            var log = CreateLog(nameof(Log.RemovedStock),removedStock);

            stock.Logs.Add(log);

            return Result.Success;
        }

        private ErrorOr<Stock> FindStock(Guid stockProductId)
        {
            var stock = Stocks.SingleOrDefault(x => x.StockProductId == stockProductId);
            if (stock == null) return Error.NotFound(code: "StockWasNotFound", description: "Stock was not found");
            return stock;
        }
        private Log CreateLog(string kindOfLog, int quantity)
        {
            return kindOfLog switch
            {
                nameof(Log.AddedStock) => new Log() { AddedStock = quantity},
                nameof(Log.RemovedStock) => new Log() { RemovedStock = quantity },
                nameof(Log.SoldStock) => new Log() { SoldStock = quantity },
            };
        }
    }
}
