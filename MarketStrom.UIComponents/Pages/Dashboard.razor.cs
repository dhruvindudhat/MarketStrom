using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class Dashboard
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }
        [Inject]
        public ModelDialogService ModelDialogService { get; set; }

        protected override void OnParametersSet()
        {
            AvailableOrders = DatabaseService.GetAvailableOrders();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public async Task SellOrder(OrderDTO order)
        {
            var result = await ModelDialogService.SellOrderDialog(order);

            if (result.Confirmed)
            {
                AvailableOrders = DatabaseService.GetAvailableOrders();
                StateHasChanged();
            }
        }
        public List<OrderDTO> AvailableOrders { get; set; } = new();
    }
}
