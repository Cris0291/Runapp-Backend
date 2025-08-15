using Ardalis.SmartEnum;

namespace RunApp.Domain.ReviewAggregate.ReviewEnum
{
    public sealed class ReviewDescriptionEnums : SmartEnum<ReviewDescriptionEnums>
    {
        public static readonly ReviewDescriptionEnums Excellent = new ReviewDescriptionEnums(nameof(Excellent), 1);
        public static readonly ReviewDescriptionEnums Good = new ReviewDescriptionEnums(nameof(Good), 2);
        public static readonly ReviewDescriptionEnums Average = new ReviewDescriptionEnums(nameof(Average), 3);
        public static readonly ReviewDescriptionEnums Incomplete = new ReviewDescriptionEnums(nameof(Incomplete), 4);
        public static readonly ReviewDescriptionEnums Terrible = new ReviewDescriptionEnums(nameof(Terrible), 5);

        private ReviewDescriptionEnums(string name, int value) : base(name, value) { }
    }

}
