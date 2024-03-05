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

            SellOrderDetails.IsByQty = AvailableStock.SubCategoryName.ToLower().Contains("quantity");
            SellOrderDetails.IsByWeight = AvailableStock.SubCategoryName.ToLower().Contains("weight");
            Customers = DatabaseService.GetAllPerson().Where(o => o.Role == (int)Role.Customer).ToList();
            Categories = DatabaseService.GetAllCategory();
            SelectedCategoy = Categories.Where(o => o.SubCategories.Where(o => o.Id == AvailableStock.SubCategoryId).FirstOrDefault() != null).FirstOrDefault();

        }

        public async Task SellOrder()
        {
            if (SellOrderDetails.Quantity > AvailableStock.AvailableQuantity)
            {
                ErrorMsg = "Max Sell Quantity Is Same As Available Sell Quantity!!";
                return;
            }
            else if (SellOrderDetails.Kg > AvailableStock.AvailableWeight)
            {
                ErrorMsg = "Max Sell Weight Is Same As Available Sell Weight!!";
                return;
            }
            else
            {
                ErrorMsg = "";
            }
            SellOrderDetails.SellOrderId = AvailableStock.Id;
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
                ResetValues();
                StateHasChanged();
            }
        }

        public void InvalidSubmission()
        {
            //   ToastService.ShowError("Please Submit Valid Details!!");
        }

        public void ResetValues()
        {
            Quantity = null;
            SellOrderDetails.Kg = null;
            SellOrderDetails.Price = 0;
            SellOrderDetails.Labour = null;
            SellOrderDetails.LabourAmount = null;
            SellOrderDetails.Comission = null;
            SellOrderDetails.ComissionAmount = null;
            SellOrderDetails.TotalAmount = 0;
            SellOrderDetails.Fare = null;
        }

        public List<Person> Customers { get; set; } = new();
        public Order SellOrderDetails { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public Category? SelectedCategoy { get; set; }

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

        private int? _quantity;
        public int? Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                SellOrderDetails.Quantity = value;
                if (SellOrderDetails.IsByQty)
                {
                    SellOrderDetails.Kg = SelectedCategoy.DefaultWeight * _quantity;
                }
            }
        }

        public string ErrorMsg { get; set; }
    }
}
