using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class LoginPage
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }

        private Login model = new Login();
        private bool loading;

        private async void OnValidSubmit()
        {
            // reset alerts on submit
            //AlertService.Clear();

            loading = true;
            NavigationManager.NavigateTo("/personlist");
            //try
            //{
            //    await AccountService.Login(model);
            //    var returnUrl = NavigationManager.QueryString("returnUrl") ?? "";
            //    NavigationManager.NavigateTo(returnUrl);
            //}
            //catch (Exception ex)
            //{
            //    AlertService.Error(ex.Message);
            //    loading = false;
            //    StateHasChanged();
            //}
        }
    }
}
