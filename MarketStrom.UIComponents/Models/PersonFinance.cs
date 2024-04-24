namespace MarketStrom.UIComponents.Models
{
    public class PersonFinance : Person
    {
        public decimal FinalAmount { get; set; }
        public DateTime PaymentDueDays { get; set; }
    }
}
