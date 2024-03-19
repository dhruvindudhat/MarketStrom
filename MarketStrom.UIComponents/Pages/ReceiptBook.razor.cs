using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class ReceiptBook
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
                    PendingOrders = DatabaseService.GetAllPurchaseOrderByPerson(_selectedPerson.Id);
                }
            }
        }
        public List<OrderDTO> PendingOrders { get; set; }
    }
}
