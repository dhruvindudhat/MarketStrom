using Blazored.Toast.Services;
using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class OrderAdd
    {
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public DatabaseService DatabaseService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected override void OnParametersSet()
        {
            Suppliers = DatabaseService.GetAllPerson().Where(o => o.Role == (int)Role.Supplier).ToList();
            if (!String.IsNullOrEmpty(Id))
            {
                int id = Int32.Parse(Id);
                Order = DatabaseService.GetOrder(id);
            }
            // base.OnParametersSet();
        }

        public async Task SaveOrder()
        {
            if (String.IsNullOrEmpty(Id))
            {
                DatabaseService.InsertOrder(Order);
            }
            else
            {
                DatabaseService.UpdateOrder(Order);
            }
            await ModalSubmit();
            NavigationManager.NavigateTo("/OrderList");
        }

        public void InvalidSubmission()
        {
            ToastService.ShowError("Please Submit Valid Details!!");
        }
        public void Restore()
        {
            Order = new();
        }

        public string CalculateTotalAmont()
        {
            double totalAmount = 0;
            if (Order.Kg != null)
            {
                totalAmount = (double)(Order.Kg * Order.Price);
            }
            else if (Order.Quantity != null)
            {
                totalAmount = (double)(Order.Quantity * Order.Price);
            }

            if (Order.Labour != null)
            {
                totalAmount = totalAmount + (double)Order.Labour;
            }
            return totalAmount.ToString();
        }


        public Order Order { get; set; } = new();
        public List<Person> Suppliers { get; set; } = new();
        public Person SelectedSupplier { get; set; }
    }
}
