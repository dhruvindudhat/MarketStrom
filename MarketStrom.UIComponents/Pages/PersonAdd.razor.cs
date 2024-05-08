﻿using Blazored.Toast.Services;
using MarketStrom.DataModels.Enums;
using MarketStrom.DataModels.Models;
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

        public async Task SavePerson()
        {
            Enum.TryParse(PersonRole, out Role role);
            if (String.IsNullOrEmpty(Id))
            {
                Person.Role = (int)role;
                DatabaseService.InsertPerson(Person);
            }
            else
            {
                DatabaseService.UpdatePerson(Person);
            }
            await ModalSubmit();
            NavigationManager.NavigateTo("/PersonList/" + PersonRole);
        }

        public void Restore()
        {
            Person = new();
        }

        public void InvalidSubmission()
        {
            ToastService.ShowError("Please Submit Valid Details!!");
        }
    }
}
