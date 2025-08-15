namespace RunApp.Domain.StoreOwnerProfileAggregate.ValueTypes
{
    public class Address
    {
        internal Address() { }
        public int ZipCode { get; internal set; }
        public string Street { get; internal set; }
        public string City { get; internal set; }
        public int BuildingNumber { get; internal set; }
        public string Country { get; internal set; }
        public string? AlternativeStreet { get; internal set; }
        public int? AlternativeBuildingNumber { get; internal set; }

    }
}
