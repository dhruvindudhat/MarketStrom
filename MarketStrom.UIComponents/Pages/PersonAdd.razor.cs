using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class PersonAdd
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
        public Person Person { get; set; } = new();

        public void SavePerson(bool IsSupplier = false)
        {
            if (String.IsNullOrEmpty(Id))
            {
                Person.Role = IsSupplier ? (int)Role.Supplier : (int)Role.Customer;
                DatabaseService.InsertPerson(Person);
            }
            else
            {
                Person.Id = Int32.Parse(Id);
                DatabaseService.UpdatePerson(Person);
            }
        }

        public void Restore()
        {
            Person = new Person();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/PersonList");
        }
    }
}
