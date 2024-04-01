using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class KhataBook : ComponentBase
    {

        [Inject]
        public DatabaseService DatabaseService { get; set; }

        protected override void OnParametersSet()
        {
            AllPerson = DatabaseService.GetAllPerson();
        }

        public List<Person> AllPerson { get; set; }

        private Person? _selectedPerson;
        public Person? SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                if (_selectedPerson != null)
                {
                    SellOrders = DatabaseService.GetAllSellOrderByPerson(_selectedPerson.Id);
                    PaymentHistory = DatabaseService.GetAllPaymentOrders(_selectedPerson.Id);
                    GroupedPaymentOrders = PaymentHistory.GroupBy(o => o.OrderIds).ToList();
                }
            }
        }

        public List<OrderDTO> SellOrders { get; set; }
        public List<PaymentHistory> PaymentHistory { get; set; }

        public List<IGrouping<string, PaymentHistory>> GroupedPaymentOrders { get; set; }
    }
}
