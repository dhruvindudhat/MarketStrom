using Blazored.Modal;
using Blazored.Modal.Services;
using MarketStrom.UIComponents.Pages;

namespace MarketStrom.UIComponents.Services
{
    public class ModelDialogService
    {
        private readonly IModalService _modelService;

        public ModelDialogService(IModalService modelService)
        {
            _modelService = modelService;
        }

        public async Task<ModalResult> WarningDialog(string title, string message)
        {
            ModalOptions options = new ModalOptions { UseCustomLayout = true };
            ModalParameters parameters = new ModalParameters();
            parameters.Add(nameof(CommonAlert.Title), title);
            parameters.Add(nameof(CommonAlert.Message), message);
            var modalReference = _modelService.Show<CommonAlert>(string.Empty, parameters, options);
            var result = await modalReference.Result;
            return result;
        }

        public async Task<ModalResult> AddUpdatePersonDialog(string id, string personRole)
        {
            ModalOptions options = new ModalOptions { UseCustomLayout = true };
            ModalParameters parameters = new ModalParameters();
            parameters.Add(nameof(PersonAdd.Id), id);
            parameters.Add(nameof(PersonAdd.PersonRole), personRole);
            var modalReference = _modelService.Show<PersonAdd>(string.Empty, parameters, options);
            var result = await modalReference.Result;
            return result;
        }

        public async Task<ModalResult> AddUpdateCategoryDialog(string id)
        {
            ModalOptions options = new ModalOptions { UseCustomLayout = true };
            ModalParameters parameters = new ModalParameters();
            parameters.Add(nameof(CategoryAdd.Id), id);
            var modalReference = _modelService.Show<CategoryAdd>(string.Empty, parameters, options);
            var result = await modalReference.Result;
            return result;
        }

        public async Task<ModalResult> AddUpdateOrderDialog(string id)
        {
            ModalOptions options = new ModalOptions { UseCustomLayout = true };
            ModalParameters parameters = new ModalParameters();
            parameters.Add(nameof(OrderAdd.Id), id);
            var modalReference = _modelService.Show<OrderAdd>(string.Empty, parameters, options);
            var result = await modalReference.Result;
            return result;
        }
    }
}
