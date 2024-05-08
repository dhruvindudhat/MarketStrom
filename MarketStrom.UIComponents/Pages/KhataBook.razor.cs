using MarketStrom.UIComponents.Constants;
using MarketStrom.DataModels.DTO;
using MarketStrom.DataModels.Enums;
using MarketStrom.DataModels.Models;
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
            AllPerson = DatabaseService.GetAllPerson().Where(o => o.Role == (int)Role.Customer).ToList();
            if (GuideContstants.KhataBookSelectedPerson != 0)
                SelectedPerson = AllPerson.Where(o => o.Id == GuideContstants.KhataBookSelectedPerson).FirstOrDefault();
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
                    GroupedPaymentOrders = PaymentHistory.Where(o => !string.IsNullOrEmpty(o.OrderIds)).GroupBy(o => o.OrderIds).ToList();
                    PendingPaymentOrders = PaymentHistory.Where(o => string.IsNullOrEmpty(o.OrderIds)).ToList();
                    GuideContstants.KhataBookSelectedPerson = _selectedPerson.Id;
                }
            }
        }

        private IReadOnlyList<DateTime?> _dateRange;
        public IReadOnlyList<DateTime?> DateRange
        {
            get { return _dateRange; }
            set
            {
                _dateRange = value;
                if (_selectedPerson != null)
                {
                    SellOrders = DatabaseService.GetAllSellOrderByPerson(_selectedPerson.Id);
                    PaymentHistory = DatabaseService.GetAllPaymentOrders(_selectedPerson.Id);
                    GroupedPaymentOrders = PaymentHistory.Where(o => !string.IsNullOrEmpty(o.OrderIds)).GroupBy(o => o.OrderIds).ToList();
                    PendingPaymentOrders = PaymentHistory.Where(o => string.IsNullOrEmpty(o.OrderIds)).ToList();
                    if (_dateRange?.Count > 1)
                    {
                        SellOrders = SellOrders.Where(o => o.CreatedOn.Date >= _dateRange[0].Value.Date && o.CreatedOn.Date <= _dateRange[1].Value.Date).ToList();
                        PaymentHistory = PaymentHistory.Where(o => o.ReceivedDate.Date >= _dateRange[0].Value.Date && o.ReceivedDate.Date <= _dateRange[1].Value.Date).ToList();
                        PendingPaymentOrders = PendingPaymentOrders.Where(o => o.ReceivedDate.Date >= _dateRange[0].Value.Date && o.ReceivedDate.Date <= _dateRange[1].Value.Date).ToList();
                    }
                    else if (_dateRange?.Count == 1)
                    {
                        SellOrders = SellOrders.Where(o => o.CreatedOn.Date == _dateRange[0].Value.Date).ToList();
                        PaymentHistory = PaymentHistory.Where(o => o.ReceivedDate.Date == _dateRange[0].Value.Date).ToList();
                        PendingPaymentOrders = PendingPaymentOrders.Where(o => o.ReceivedDate.Date == _dateRange[0].Value.Date).ToList();
                    }
                    StateHasChanged();
                }
            }
        }
        public List<OrderDTO> SellOrders { get; set; }
        public List<PaymentHistory> PaymentHistory { get; set; }
        public List<PaymentHistory> PendingPaymentOrders { get; set; } //Order which is not map with communitive balance
        public List<IGrouping<string, PaymentHistory>> GroupedPaymentOrders { get; set; }
    }
}
