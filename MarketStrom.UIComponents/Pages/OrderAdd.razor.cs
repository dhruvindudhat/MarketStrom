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
            Categories = DatabaseService.GetAllCategory();
            SelectedCategoy = Categories.FirstOrDefault();
            if (!String.IsNullOrEmpty(Id))
            {
                int id = Int32.Parse(Id);
                Order = DatabaseService.GetOrder(id);
                SelectedSupplier = Suppliers.Where(o => o.Id == Order.PersonId).FirstOrDefault();
            }
        }

        public async Task SaveOrder()
        {
            Order.PersonId = SelectedSupplier.Id;
            Order.SubCategoryId = SelectedSubCategoy.Id;
            Order.CreatedOn = DateTime.Now;
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

        public Order Order { get; set; } = new();
        public List<Person> Suppliers { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<SubCategory> SubCategories { get; set; } = new();
        public Models.SubCategory? SelectedSubCategoy { get; set; } = new();

        private Category? _selectedCategory;
        public Category? SelectedCategoy
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                SelectedSubCategoy = _selectedCategory?.SubCategories.FirstOrDefault();
            }
        }

        private Person? _selectedSupplier;
        public Person? SelectedSupplier
        {
            get { return _selectedSupplier; }
            set
            {
                _selectedSupplier = value;
                Order.PersonId = _selectedSupplier.Id;
            }
        }
    }
}
