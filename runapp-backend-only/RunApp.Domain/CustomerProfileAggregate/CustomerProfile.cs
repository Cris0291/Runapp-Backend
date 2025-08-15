using ErrorOr;
using RunApp.Domain.Common;
using RunApp.Domain.Common.ValueType;



namespace RunApp.Domain.CustomerProfileAggregate
{
    public class CustomerProfile : Entity
    {
        internal CustomerProfile() { }

        public List<Guid> Reviews { get; internal set; } = new();
        public List<Guid> Ratings { get; internal set; } = new();
        public List<Guid> Statuses { get; internal set; } = new();
        public List<Guid> BoughtProducts { get; internal set; } = new();
        public List<Guid> CreatedProducts { get; internal set; } = new();
        public List<Guid> Orders { get; internal set; } = new();
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string NickName { get; private set; }
        public Address? ShippingAdress { get; private set; }
        public Card? PaymentMethod { get; private set; }
        public Guid Id { get; private set; }

        public static ErrorOr<CustomerProfile> CreateCustomerProfile(string name, string email, string nickname, Guid id)
        {

            return new CustomerProfile
            {
                Name = name,
                Email = email,
                NickName = nickname,
                Id = id,
                Reviews = new(),
                Ratings = new(),
                Statuses = new(),
                BoughtProducts = new(),
                CreatedProducts = new(),
                Orders = new(),
            };

        }
        public Address AddAddress(string zipCode, string street, string city, string country, string state)
        {
            ShippingAdress = new Address
            {
                ZipCode = zipCode,
                Street = street,
                City = city,
                Country = country,
                State = state
            };

            return ShippingAdress;
        }
        public Address UpdateAddress(string zipCode, string street, string city, string country, string state)
        {
            ShippingAdress!.ZipCode = zipCode;
            ShippingAdress.Street = street;
            ShippingAdress.City = city;
            ShippingAdress.Country = country;
            ShippingAdress.State = state;

            return ShippingAdress;
        }
        public Card AddPaymentMethod(string holdersName, string cardNumber, string cvv, string expiryDate)
        {
            PaymentMethod = new Card
            {
                CVV = cvv,
                HoldersName = holdersName,
                CardNumber = cardNumber,
                ExpiryDate = expiryDate
            };

            return PaymentMethod;
        }
        public Card UpdatePaymentMethod(string holdersName, string cardNumber, string cvv, string expiryDate)
        {
            PaymentMethod!.CVV = cvv;
            PaymentMethod.HoldersName = holdersName;
            PaymentMethod.CardNumber = cardNumber;
            PaymentMethod.ExpiryDate = expiryDate;

            return PaymentMethod;
        }

        public void UpdateAccountInfo(string name, string email, string nickName)
        {
            Name = name;
            Email = email;
            NickName = nickName;
        }
        public bool IsProductBought(Guid productId)
        {
            return BoughtProducts.Contains(productId);
        }
        public void AddReview(Guid productId)
        {
            if (Reviews.Contains(productId)) throw new InvalidOperationException("Cannot add more than one review per user");

            Reviews.Add(productId);
        }
        public void DeleteReview(Guid reviewId)
        {
            var wasRemoved = Reviews.Remove(reviewId);
            if (!wasRemoved) throw new InvalidOperationException("Review was not removerd");
        }
        public void AddProductStatus(Guid productStatusId)
        {
            if (Statuses.Contains(productStatusId)) throw new InvalidOperationException("Cannot add more than one like or dislike per user");

            Statuses.Add(productStatusId);
        }

        public void AddRating(Guid raitingId)
        {
            if (Ratings.Contains(raitingId)) throw new InvalidOperationException("Cannot rate twice a product");
            Ratings.Add(raitingId);
        }
        public void AddOrder(Guid orderId)
        {
            if(Orders.Contains(orderId)) throw new InvalidOperationException("Cannot add the same order twice");
            Orders.Add(orderId);
        }
        public void AddBoughtProducts(IEnumerable<Guid> boughtProducts)
        {
            foreach (var product in boughtProducts)
            {
                BoughtProducts.Add(product);
            }
        }
        public void AddCreatedProduct(Guid productId)
        {
            if(CreatedProducts.Contains(productId)) throw new InvalidOperationException("Something unexpected happened, cannot create the same product twice");
            CreatedProducts.Add(productId);
        }
    }
}
