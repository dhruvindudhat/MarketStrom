using Blazored.Toast.Services;
using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class OrderList
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }
        [Inject]
        public ModelDialogService ModelDialogService { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }

        protected override void OnParametersSet()
        {
            Orders = DatabaseService.GetAllOrders();
        }

        public async Task AddOrder(int id)
        {
            var result = await ModelDialogService.AddUpdateOrderDialog((id == 0) ? string.Empty : id.ToString());
            if (result.Confirmed)
            {
                Orders = DatabaseService.GetAllOrders();
                StateHasChanged();
                ToastService.ShowSuccess((id != 0) ? "Order Updated SuccessFully!!" : "Order Added SuccessFully!!");
            }
        }

        public async Task DeleteOrder(int orderId)
        {
            var result = await ModelDialogService.WarningDialog("Warning", string.Format("Do You Want To Delete This Order?"));

            if (result.Confirmed)
            {
                DatabaseService.DeleteOrder(orderId);
                Orders = DatabaseService.GetAllOrders();
                ToastService.ShowSuccess("Order Deleted SuccessFully!!");
            }
        }

        public List<OrderDTO> Orders { get; set; }

        public Order Order { get; set; } = new();
    }
}
