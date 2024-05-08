using MarketStorm.Report;
using MarketStrom.UIComponents.Constants;
using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

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
            ReportController.OpenPDFReport();
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

