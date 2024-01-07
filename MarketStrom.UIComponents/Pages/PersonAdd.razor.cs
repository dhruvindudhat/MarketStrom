using Blazored.Toast.Services;
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

        [Inject]
        public IToastService ToastService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string PersonRole { get; set; }

        protected override void OnParametersSet()
        {
            if (!String.IsNullOrEmpty(Id))
            {
                int id = Int32.Parse(Id);
                Person = DatabaseService.GetPerson(id);
            }
            base.OnParametersSet();
        }

        public Person Person { get; set; } = new();

        public void SavePerson()
        {
            Enum.TryParse(PersonRole, out Role role);
            if (String.IsNullOrEmpty(Id))
            {
                Person.Role = (int)role;
                DatabaseService.InsertPerson(Person);
                ToastService.ShowSuccess(PersonRole + " Added SuccessFully!!");
            }
            else
            {
                DatabaseService.UpdatePerson(Person);
                ToastService.ShowSuccess(PersonRole + " Updated SuccessFully!!");
            }
            NavigationManager.NavigateTo("/PersonList/" + PersonRole);
        }

        public void Restore()
        {
            Person = new Person();
        }

        public void InvalidSubmission()
        {
            // StateHasChanged();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/PersonList/" + PersonRole);
        }
    }
}
