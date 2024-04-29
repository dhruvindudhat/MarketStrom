using MarketStrom.UIComponents.Constants;
using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class PurchaseBook
    {


        [Inject]
        public DatabaseService DatabaseService { get; set; }

        protected override void OnParametersSet()
        {
            AllSellers = DatabaseService.GetAllPerson().Where(o => o.Role == (int)Role.Supplier).ToList();
            //if (GuideContstants.KhataBookSelectedPerson != 0)
            //    SelectedPerson = AllSellers.Where(o => o.Id == GuideContstants.KhataBookSelectedPerson).FirstOrDefault();
        }

        private Person? _selectedPerson;
        public Person? SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                if (_selectedPerson != null)
                {
                    SellOrders = DatabaseService.GetAllPurchaseOrderByPerson(_selectedPerson.Id);
                    PaymentHistory = DatabaseService.GetAllPaymentOrders(_selectedPerson.Id);
                    GroupedPaymentOrders = PaymentHistory.Where(o => !string.IsNullOrEmpty(o.OrderIds)).GroupBy(o => o.OrderIds).ToList();
                    PendingPaymentOrders = PaymentHistory.Where(o => string.IsNullOrEmpty(o.OrderIds)).ToList();
                    // GuideContstants.KhataBookSelectedPerson = _selectedPerson.Id;
                }
            }
        }

        public List<Person> AllSellers { get; set; }
        public List<OrderDTO> SellOrders { get; set; }
        public List<PaymentHistory> PaymentHistory { get; set; }
        public List<PaymentHistory> PendingPaymentOrders { get; set; } //Order which is not map with communitive balance
        public List<IGrouping<string, PaymentHistory>> GroupedPaymentOrders { get; set; }
    }
}
