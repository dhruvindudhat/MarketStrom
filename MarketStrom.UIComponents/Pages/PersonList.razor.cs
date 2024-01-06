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
        [Parameter]
        public string PersonRole { get; set; }

        protected override void OnParametersSet()
        {
            AllPerson = GetAllPerson();
        }

        protected async override Task OnInitializedAsync()
        {
            DatabaseService.Load(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MarketStorm", "default.mkt"));
            //AllPerson = GetAllPerson();
        }

        public void DeleteCustomer(int id)
        {
            //TODO:ADD DELETE CONFIRMATION POPUP
            DatabaseService.DeleteRecord(id, "Person");
            AllPerson = GetAllPerson();
        }

        private List<Person> GetAllPerson()
        {
            Enum.TryParse(PersonRole, out Role role);
            return DatabaseService.GetAllPerson().Where(o => o.IsDeleted == false && o.Role == (int)role).ToList();
        }

        public List<Person> AllPerson { get; set; }
    }
}
