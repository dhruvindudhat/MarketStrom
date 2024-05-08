using MarketStrom.DataModels.Enums;

namespace MarketStrom.DataModels.Models
{
    public class PaymentDetails
    {
        public double PaidAmount { get; set; }
        public PaymentMode SelectedPaymentMode { get; set; }
    }
}
