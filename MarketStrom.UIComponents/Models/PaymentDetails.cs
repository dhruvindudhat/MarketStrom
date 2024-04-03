using MarketStrom.UIComponents.Enums;

namespace MarketStrom.UIComponents.Models
{
    public class PaymentDetails
    {
        public double PaidAmount { get; set; }
        public PaymentMode SelectedPaymentMode { get; set; }
    }
}
