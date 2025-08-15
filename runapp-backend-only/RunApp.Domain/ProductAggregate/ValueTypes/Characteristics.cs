namespace RunApp.Domain.ProductAggregate.ValueTypes
{
    public class Characteristics
    {
        internal Characteristics() { }
        public string Brand { get; internal set; }
        public string Type { get; internal set; }
        public string Color { get; internal set; }
        public double Weight { get; internal set; }

    }
}
