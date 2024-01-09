using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class CommonAlert
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Message { get; set; }
    }
}
