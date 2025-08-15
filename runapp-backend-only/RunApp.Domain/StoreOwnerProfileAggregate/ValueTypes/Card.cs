namespace RunApp.Domain.StoreOwnerProfileAggregate.ValueTypes
{
    public class Card
    {
        internal Card() { }
        public string HoldersName { get; internal set;}
        public int CardNumber { get; internal set; }
        public int CVV { get; internal set; }
        public DateTime ExpityDate { get; internal set; }
    }
}
