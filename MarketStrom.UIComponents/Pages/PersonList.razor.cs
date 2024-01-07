using Blazored.Toast.Services;
using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class PersonList
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IToastService ToastService { get; set; }
        [Parameter]
        public string PersonRole { get; set; }

        protected AlertBase AlertBase { get; set; }

        protected override void OnParametersSet()
        {
            AllPerson = GetAllPerson();
        }

        protected async override Task OnInitializedAsync()
        {
            DatabaseService.Load(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MarketStorm", "default.mkt"));
        }

        private void ConfirmDelete(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                DatabaseService.DeleteRecord(PersonId, "Person");
                AllPerson = GetAllPerson();
                ToastService.ShowSuccess(PersonRole + " Deleted SuccessFully!!");
            }
        }

        public void DeleteCustomer(int id)
        {
            AlertBase.Show();
            PersonId = id;
        }

        private List<Person> GetAllPerson()
        {
            Enum.TryParse(PersonRole, out Role role);
            return DatabaseService.GetAllPerson().Where(o => o.IsDeleted == false && o.Role == (int)role).ToList();
        }

        public List<Person> AllPerson { get; set; }
        public int PersonId { get; set; }
    }
}
