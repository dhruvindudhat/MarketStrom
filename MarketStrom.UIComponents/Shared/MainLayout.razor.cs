using Blazored.Toast.Services;
using MarketStrom.DataModels.Models;
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
        [Inject]
        public IToastService ToastService { get; set; }

        protected override void OnInitialized()
        {
            DatabaseService.Load(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MarketStorm", "default.mkt"));
        }

        private async Task OnValidSubmit()
        {
            Login user = DatabaseService.GetUser(model.Username);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    IsLogin = true;
                }
                else
                {
                    ToastService.ShowError("Wrong Password!! Please Enter Valid Password.");
                }
            }
            else
            {
                ToastService.ShowError("User Not Exist!! Please Enter Valid Username.");
            }
        }

        private void Adduser()
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            DatabaseService.SaveUser(model);
            StateHasChanged();
        }

        private Login model = new Login();
        public bool IsLogin { get; set; }
    }
}
