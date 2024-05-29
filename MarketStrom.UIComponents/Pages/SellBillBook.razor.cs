using MarketStorm.Report;
using MarketStrom.UIComponents.Constants;
using MarketStrom.DataModels.DTO;
using MarketStrom.DataModels.Enums;
using MarketStrom.DataModels.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;
using MarketStorm.DataModels.Models;

namespace MarketStrom.UIComponents.Pages
{
    public partial class SellBillBook
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }
        [Inject]
        public ReportController ReportController { get; set; }

        protected override void OnParametersSet()
        {
            AllPerson = DatabaseService.GetAllPerson().Where(o => o.Role == (int)Role.Customer).ToList();
        }

        private void BillGenerate()
        {
            SellBillInformation info = new SellBillInformation()
            {
                PersonName = _selectedPerson.FirstName + " " + _selectedPerson.LastName,
                CompanyName = "JALARAM TRADING CO",
                Address = "SHOP NO G 1 MARKETING YARD HAPA JAMNAGAR",
                MobileNumber = "MO 9723489567 / 9714000260 / 9913166453",
                BankName = "BANK OF INDIA",
                AccountNumber = "325630110000070",
                IFSCCode = "BKID0003256"
            };
            ReportController.GenerateSellBill(SellOrders, info);
        }

        private Person? _selectedPerson;
        public Person? SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                if (_selectedPerson != null && _selectedDate != null)
                {
                    SellOrders = DatabaseService.GetAllSellOrderByPersonAndDate(_selectedPerson.Id, _selectedDate.Value);
                }
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                if (_selectedDate != null && _selectedPerson != null)
                {
                    SellOrders = DatabaseService.GetAllSellOrderByPersonAndDate(_selectedPerson.Id, _selectedDate.Value);
                }
            }
        }

        public List<OrderDTO> SellOrders { get; set; } = new();
        public List<Person> AllPerson { get; set; }
    }
}

