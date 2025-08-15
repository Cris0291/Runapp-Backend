using RunApp.Domain.Common;
using RunApp.Domain.CustomerProfileAggregate;
using RunApp.Domain.Products;
using RunApp.Domain.ReviewAggregate.Events;
using RunApp.Domain.ReviewAggregate.ReviewEnum;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestsUtilities")]
namespace RunApp.Domain.ReviewAggregate
{
    public class Review : Entity
    {
        internal Review() { }
        public Guid ReviewId { get; internal set; }
        public string Comment { get; internal set; }
        public int Rating { get; internal set; }
        public DateTime Date { get; internal set; }
        public ReviewDescriptionEnums ReviewDescription { get; internal set; }
        public Guid ProductId { get; internal set; }
        public Guid Id { get; internal set; }
        public static Review CreateReview(string comment, int rating, ReviewDescriptionEnums reviewDescription, Guid productId, Guid customerProfileId)
        {
            var review = new Review()
            {
                ReviewId = Guid.NewGuid(),
                Id = customerProfileId,
                Comment = comment,
                ReviewDescription = reviewDescription,
                ProductId = productId,
                Rating = rating,
            };

            review.RaiseEvent(new AddReviewEvent(review, productId, customerProfileId));
            return review;
        }

        public void RemoveReview(Guid reviewId, Guid productId, Guid customerProfileId)
        {
            RaiseEvent(new DeleteReviewEvent(this, productId, customerProfileId));
        }
        public void UpdateReview(Guid productId, Guid customerProfileId, string comment, int rating, ReviewDescriptionEnums reviewDescription)
        {
            int oldRating = Rating;

            Comment = comment;
            ReviewDescription = reviewDescription;
            Rating = rating;

            RaiseEvent(new UpdateReviewEvent(this, productId, customerProfileId, oldRating));
        }
    }
}
