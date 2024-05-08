using Blazored.Modal;
using Blazored.Modal.Services;
using MarketStrom.DataModels.Models;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Base
{
    public class PopupMasterComponent : ComponentBase
    {
        #region ServiceInjection

        [CascadingParameter]
        BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [Parameter]
        public EventCallback<bool> OnClose { get; set; }

        #endregion

        #region LifeCycleMethods

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Cancel and close dialog
        /// </summary>
        /// <returns></returns>
        public async Task ModalCancel()
        {
            //Data is Null in ModalResult for "Cancel"
            await BlazoredModal.CancelAsync();
        }

        /// <summary>
        /// Submit and close dialog
        /// </summary>
        /// <returns></returns>
        public async Task ModalSubmit()
        {
            //Return ModelData as True to identify "Yes" Button Clicked
            await BlazoredModal.CloseAsync(ModalResult.Ok(true));
        }

        public async Task ModalSubmitWithData(PaymentDetails amount)
        {
            //Return ModelData as True to identify "Yes" Button Clicked
            await BlazoredModal.CloseAsync(ModalResult.Ok(amount));
        }

        /// <summary>
        /// Set Response as No and close dialog
        /// </summary>
        /// <returns></returns>
        public async Task ModalNo()
        {
            //Return ModelData as False to identify "No" Button Clicked
            await BlazoredModal.CloseAsync(ModalResult.Ok(false));
        }

        #endregion
    }
}
