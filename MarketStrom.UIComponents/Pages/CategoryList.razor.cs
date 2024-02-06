using Blazored.Toast.Services;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class CategoryList
    {

        [Inject]
        public DatabaseService DatabaseService { get; set; }
        [Inject]
        public ModelDialogService ModelDialogService { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }

        protected override void OnParametersSet()
        {
            Catogories = DatabaseService.GetAllCategory();
        }

        public async Task AddCategory(int id)
        {
            var result = await ModelDialogService.AddUpdateCategoryDialog((id == 0) ? string.Empty : id.ToString());
            if (result.Confirmed)
            {
                Catogories = DatabaseService.GetAllCategory();
                StateHasChanged();
                ToastService.ShowSuccess(((id != 0) ? "Category Updated SuccessFully!!" : "Category Added SuccessFully!!"));
            }
        }

        public async Task DeleteCategory(Category category)
        {
            var result = await ModelDialogService.WarningDialog("Warning", string.Format("Do you want to delete {0} Category? ", category.Name));

            if (result.Confirmed)
            {
                DatabaseService.DeleteCategory(category);
                Catogories = DatabaseService.GetAllCategory();
                ToastService.ShowSuccess(category.Name + " Deleted SuccessFully!!");
            }
        }
        public List<Category> Catogories { get; set; }
        public Category Category { get; set; } = new();
    }
}
