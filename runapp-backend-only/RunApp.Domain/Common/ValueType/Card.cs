namespace RunApp.Domain.Common.ValueType
{
    public class Card
    {
        public Card() { }
        public string HoldersName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryDate { get; set; }
    }
}
