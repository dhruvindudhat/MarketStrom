using MarketStrom.UIComponents.Base;
using MarketStrom.UIComponents.Models;

namespace MarketStrom.UIComponents.Pages
{
    public partial class Payment : PopupMasterComponent
    {

        public async Task SavePayment()
        {
            await ModalSubmitWithAmount(PaidAmount);
        }

        public void InvalidSubmission()
        {

        }

        public double PaidAmount { get; set; }
    }
}
