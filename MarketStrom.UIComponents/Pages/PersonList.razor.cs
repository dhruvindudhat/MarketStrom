using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class PersonList
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }

        protected async override Task OnInitializedAsync()
        {
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
            //DatabaseService.InsertPerson(person);
            //DatabaseService.InsertCategory(category);
            //DatabaseService.InsertSubCategory(subCategory);
            //DatabaseService.InsertSubCategory(subCategory2);
            Customers = DatabaseService.GetAllPerson();
            //db.DeleteRecord(subCategory.Id, "SubCategory");
            var result = DatabaseService.GetAllCategory();

            base.OnInitializedAsync();
        }

        public List<Person> Customers { get; set; }
        public Blazorise.DataGrid.DataGrid<Person> bgrid { get; set; }
    }
}
