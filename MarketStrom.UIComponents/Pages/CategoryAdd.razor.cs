﻿using Blazored.Toast.Services;
using MarketStrom.DataModels.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class CategoryAdd
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
            if (!String.IsNullOrEmpty(Id))
            {
                int id = Int32.Parse(Id);
                Category = DatabaseService.GetCategory(id);
            }
            else
            {
                Category.SubCategories = new List<DataModels.Models.SubCategory>()
            {
                new DataModels.Models.SubCategory(){Category = Category,CategoryId = Category.Id,Name = "By Quantity"},
                new DataModels.Models.SubCategory(){Category = Category,CategoryId = Category.Id,Name = "By Weight"},
            };
            }
            base.OnParametersSet();
        }

        public async Task SaveCategory()
        {
            if (String.IsNullOrEmpty(Id))
            {
                DatabaseService.InsertCategory(Category);
            }
            else
            {
                DatabaseService.UpdateCategory(Category);
            }
            await ModalSubmit();
            NavigationManager.NavigateTo("/CategoryList");
        }

        public void Restore()
        {
            Category = new();
        }

        public void AddDefaultSubCategory()
        {
            Category.SubCategories.Add(new DataModels.Models.SubCategory());
        }

        public void DeleteSubCategory(DataModels.Models.SubCategory subcategory)
        {
            Category.SubCategories.Remove(subcategory);
        }

        public void InvalidSubmission()
        {
            ToastService.ShowError("Please Submit Valid Details!!");
        }

        public Category Category { get; set; } = new();
    }
}
