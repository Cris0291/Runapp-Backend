
namespace RunApp.Domain.ProductAggregate.Categories
{
    public class Category
    {
        internal Category() { }
        public Guid CategoryId { get; internal set; }
        public string CategoryName { get; internal set; }

        internal static string[] validCategories = { "Equipment", "Apparel", "Nutrition", "Cardio", "Wellness", "Supplements", "Yoga", "HIIT", "Weight Loss", "Group Fitness" };

    }
}
