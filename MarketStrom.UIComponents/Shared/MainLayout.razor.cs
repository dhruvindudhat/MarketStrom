using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MarketStrom.UIComponents.Shared
{
    public partial class MainLayout
    {

        [Inject]
        public DatabaseService DatabaseService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        private async Task OnValidSubmit()
        {
            IsLogin = true;
        }

        private Login model = new Login();
        public bool IsLogin { get; set; }
    }
}
