using MarketStrom.UIComponents.Base;
using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;

namespace MarketStrom.UIComponents.Pages
{
    public partial class Payment : PopupMasterComponent
    {

        public async Task SavePayment()
        {
            PaymentDetails details = new PaymentDetails()
            {
                PaidAmount = PaidAmount,
                SelectedPaymentMode = SelectedPaymentMode
            };
            await ModalSubmitWithData(details);
        }

        public void InvalidSubmission()
        {

        }

        public double PaidAmount { get; set; }
        public List<PaymentMode> PaymentModes => Enum.GetValues(typeof(PaymentMode)).Cast<PaymentMode>().ToList();

        public PaymentMode SelectedPaymentMode { get; set; } = PaymentMode.Cash;
    }
}
