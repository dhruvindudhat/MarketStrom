using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class Dashboard
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }

        protected override void OnParametersSet()
        {
            AvailableOrders = DatabaseService.GetAvailableOrders();
        }

        public List<OrderDTO> AvailableOrders { get; set; } = new();
    }
}
