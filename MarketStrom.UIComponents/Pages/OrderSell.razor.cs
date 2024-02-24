using Blazored.Toast.Services;
using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class OrderSell
    {
        [Parameter]
        public OrderDTO AvailableStock { get; set; }
        [Inject]
        public DatabaseService DatabaseService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }


        protected override void OnParametersSet()
        {
            Customers = DatabaseService.GetAllPerson().Where(o => o.Role == (int)Role.Customer).ToList();
        }

        public async Task SellOrder()
        {
            if (SellOrderDetails.Quantity > AvailableStock.Quantity)
            {
                ErrorMsg = "Max Sell Quantity Is Same As Available Sell Quantity!!";
                return;
            }
            else if (SellOrderDetails.Kg > AvailableStock.Kg)
            {
                ErrorMsg = "Max Sell Weight Is Same As Available Sell Weight!!";
                return;
            }
            else
            {
                ErrorMsg = "";
            }
            SellOrderDetails.SubCategoryId = AvailableStock.SubCategoryId;
            SellOrderDetails.CreatedOn = DateTime.Now;
            SellOrderDetails.TotalAmount = -1 * SellOrderDetails.TotalAmount;
            if (SellOrderDetails.Quantity != null)
                SellOrderDetails.Quantity = -1 * SellOrderDetails.Quantity;
            if (SellOrderDetails.Kg != null)
                SellOrderDetails.Kg = -1 * SellOrderDetails.Kg;
            DatabaseService.InsertOrder(SellOrderDetails);
            AvailableStock = DatabaseService.GetAvailableOrders().Where(o => o.SubCategoryId == SellOrderDetails.SubCategoryId).FirstOrDefault();
            if (AvailableStock == null)
            {
                await ModalSubmit();
                NavigationManager.NavigateTo("/");
                ToastService.ShowSuccess("All Stock Sell Successfully!!");
            }
            else
            {
                StateHasChanged();
            }
        }

        public void InvalidSubmission()
        {
            //   ToastService.ShowError("Please Submit Valid Details!!");
        }

        public List<Person> Customers { get; set; } = new();
        public Order SellOrderDetails { get; set; } = new();

        private Person? _selectedCustomer;
        public Person? SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                SellOrderDetails.PersonId = _selectedCustomer.Id;
            }
        }

        public string ErrorMsg { get; set; }
    }
}
