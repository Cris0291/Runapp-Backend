using RunApp.Domain.ProductAggregate.ValueType;
using ErrorOr;
using RunApp.Domain.ProductAggregate.ProductErrors;
using System.Runtime.CompilerServices;
using RunApp.Domain.ProductAggregate.ValueTypes;
using RunApp.Domain.Common;
using RunApp.Domain.ProductAggregate.Events;
using RunApp.Domain.ReviewAggregate;
using RunApp.Domain.ProductAggregate.Categories;


[assembly: InternalsVisibleTo("TestsUtilities")]
namespace RunApp.Domain.Products
{
    public class Product : Entity
    {
       
        internal Product() { }
         // Constructor use for unit testing
        internal Product(Guid productId, string name, decimal actualPrice, string description, PriceOffer priceOffer, ICollection<About> bulletpoints, List<Review> reviews)
        {
            ProductId = productId;
            Name = name;
            ActualPrice = actualPrice;
            Description = description;
            PriceOffer = priceOffer;
            BulletPoints = bulletpoints;
        }
        private int _averageSum;
        public List<Guid> Reviews { get; internal set; }
        public List<Guid> Ratings { get; internal set; }
        public Guid ProductId { get; internal set; }
        public string Name { get;  internal set; }
        public decimal ActualPrice { get; internal set; }
        public string Description { get; internal set; }
        public int NumberOfReviews { get; internal set; }
        public double AverageRatings { get; internal set; }
        public int NumberOfLikes { get; internal set; }
        public PriceOffer? PriceOffer { get; internal set; }
        public Characteristics Characteristic { get; internal set; }
        public ICollection<About> BulletPoints { get; internal set; }
        public ICollection<Category> Categories { get; internal set; }


        public static ErrorOr<Product> CreateProduct(string name, string description, decimal price, ICollection<string> bulletpoints, decimal? priceWithDiscount, string? promotionalText, string brand, string type, string color, double weight, Guid userId)
        {
            decimal maximumDiscount = 0.7m;
            AddValidation(nameof(ProductError.DiscountPricesMustBeMaximum70Percent), () => priceWithDiscount < price - (price * maximumDiscount));
            AddValidation(nameof(ProductError.BulletPointsCollectionShoulNotBeEmpty), () => !bulletpoints.Any());
            AddValidation(nameof(ProductError.ActualPriceCannotBeLowerThanPriceWithDiscount),() => priceWithDiscount.HasValue && price < priceWithDiscount.Value);
            AddValidation(nameof(ProductError.AllPricesWithDiscountMustHaveAPromotionalText), () => priceWithDiscount != null && string.IsNullOrEmpty(promotionalText));
            AddValidation(nameof(ProductError.ProductWeightCannotBeGreaterThan200Kilograms), () => weight > 200);
            Validate();
            if (HasError()) return Errors;

            PriceOffer? priceOffer = null;

            if (priceWithDiscount != null && promotionalText != null) priceOffer = new PriceOffer { PriceWithDiscount = priceWithDiscount.Value, PromotionalText = promotionalText };


            var result =  new Product
            {
                Name = name,
                ActualPrice = price,
                Description = description,
                BulletPoints = bulletpoints.Select(point => new About() { BulletPoint = point }).ToList(),
                PriceOffer = priceOffer,
                Characteristic = new Characteristics() { Brand = brand, Type = type, Color = color, Weight = weight},
                Reviews = new(),
                Ratings = new(),
            };

            result.RaiseEvent(new CreatedProductEvent(result, userId));
            return result;
        }

        public ErrorOr<Success> UpdateProduct(string name, string description, decimal price, ICollection<string> bulletpoints, string brand, string type, string color, double weight)
        {
            decimal maximumDiscount = 0.7m;
            AddValidation(nameof(ProductError.BulletPointsCollectionShoulNotBeEmpty), () =>!bulletpoints.Any());
            AddValidation(nameof(ProductError.ActualPriceCannotBeLowerThanPriceWithDiscount), () => PriceOffer != null && PriceOffer.PriceWithDiscount.HasValue && price < PriceOffer.PriceWithDiscount.Value);
            AddValidation(nameof(ProductError.DiscountPricesMustBeMaximum70Percent), () => PriceOffer != null && PriceOffer.PriceWithDiscount.HasValue && PriceOffer.PriceWithDiscount.Value < price - (price * maximumDiscount));
            AddValidation(nameof(ProductError.ProductWeightCannotBeGreaterThan200Kilograms), () => weight > 200);
            Validate();
            if (HasError()) return Errors;

            Name = name;
            ActualPrice = price;
            Description = description;
            BulletPoints = bulletpoints.Select(point => new About() { BulletPoint = point }).ToList();
            Characteristic = new Characteristics() { Brand = brand, Type = type, Color = color, Weight = weight };

            return Result.Success;
        }
        public void DeleteProduct(Guid userId)
        {
            RaiseEvent(new DeleteProductEvent(ProductId, userId));
        }

        public ErrorOr<Success> AddPriceWithDiscount(decimal priceWithDiscount, string promotionalText)
        {
            decimal maximumDiscount = 0.7m;
            AddValidation(nameof(ProductError.DiscountPricesMustBeMaximum70Percent),() => priceWithDiscount < ActualPrice - (ActualPrice * maximumDiscount));
            AddValidation(nameof(ProductError.ActualPriceCannotBeLowerThanPriceWithDiscount), () => ActualPrice < priceWithDiscount);
            Validate();
            if (HasError()) return Errors;


            PriceOffer = new PriceOffer { PriceWithDiscount = priceWithDiscount, PromotionalText = promotionalText };
          

            return Result.Success;
        }

        public void RemovePriceWithDiscount()
        {
            PriceOffer = null;
            
        }
        public void AddReview(Review review)
        {
            if (Reviews.Contains(review.ReviewId)) throw new InvalidOperationException("Cannot add more than one review per user");
            Reviews.Add(review.ReviewId);

            NumberOfReviews = Reviews.Count();

            _averageSum += review.Rating;
            AverageRatings = _averageSum / NumberOfReviews;
        }
        public void DeleteReview(Review review)
        {
            var wasRemoved = Reviews.Remove(review.ReviewId);
            NumberOfReviews = Reviews.Count();
            if (!wasRemoved) throw new InvalidOperationException("Review was not removerd");

            _averageSum -= review.Rating;
            AverageRatings = _averageSum / NumberOfReviews;

        }
        public void UpdateReview(Review review, int oldRating)
        {
            if (!Reviews.Contains(review.ReviewId)) throw new InvalidOperationException("Review was not found");

            _averageSum -= oldRating;
            _averageSum += review.Rating;
            AverageRatings = _averageSum / NumberOfReviews;
        }
        public void AddProductLike(bool like)
        {
            NumberOfLikes = like ? NumberOfLikes++ : NumberOfLikes == 0 ? NumberOfLikes : NumberOfLikes--;
        }
        public ErrorOr<Category> AddCategory(string category)
        {
            if (!Category.validCategories.Contains(category)) return Error.Validation(code: "CategoryWasNotValid", description: "Category was not valid");
            var categoryToAdd = new Category { CategoryName = category };

            if (Categories != null) 
            {
                if (Categories.Where(x => x.CategoryName == category).Count() > 0) return Error.Validation(code: "CategoryWasAlreadyAdded", description: "Cannot add the same category more than one time");
                Categories.Add(categoryToAdd);
                return categoryToAdd;
            }

            Categories = [];
            Categories.Add(categoryToAdd);
            return categoryToAdd;
        }
        public Category? DeleteCategory(Guid categoryId)
        {
            if (Categories.Count > 1) throw new InvalidOperationException("Cannot repeat categories");

            return Categories.SingleOrDefault(x => x.CategoryId == categoryId);
        }
    }
}
