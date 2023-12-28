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
            DatabaseService = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MarketStorm", "default.mkt"));
            Person person = new Person()
            {
                FirstName = "Dhruvin",
                LastName = "Dudhat",
                MobileNo = "7575088440",
                CreatedOn = DateTime.Now,
                CreatedBy = 1
            };
            Category category = new Category() { Name = "Potato" };
            SubCategory subCategory = new SubCategory() { Name = "Vikas", CategoryId = 1 };
            SubCategory subCategory2 = new SubCategory() { Name = "Ujjval", CategoryId = 1 };
            DatabaseService.InsertPerson(person);
            DatabaseService.InsertCategory(category);
            DatabaseService.InsertSubCategory(subCategory);
            DatabaseService.InsertSubCategory(subCategory2);

            //db.DeleteRecord(subCategory.Id, "SubCategory");
            var result = DatabaseService.GetAllCategory();

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
