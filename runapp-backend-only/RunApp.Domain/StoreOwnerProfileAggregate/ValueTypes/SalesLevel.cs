using Ardalis.SmartEnum;

namespace RunApp.Domain.StoreOwnerProfileAggregate.ValueTypes
{
    public sealed class SalesLevel : SmartEnum<SalesLevel>
    {
        public static readonly SalesLevel Junior = new SalesLevel(nameof(Junior), 0);
        public static readonly SalesLevel Intermediate = new SalesLevel(nameof(Intermediate), 1);
        public static readonly SalesLevel Senior = new SalesLevel(nameof(Senior), 2);

        private SalesLevel(string name, int value) : base(name, value) { }
    }
}
