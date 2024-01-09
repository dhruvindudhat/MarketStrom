using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class CategoryList
    {

        [Inject]
        public DatabaseService DatabaseService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Catogories = DatabaseService.GetAllCategory();
        }

        public List<Category> Catogories { get; set; }
        public Category Category { get; set; } = new();
    }
}
