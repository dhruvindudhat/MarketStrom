using MarketStrom.DataModels.Models;
using MarketStrom.UIComponents.Base;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class PurchasePriceDialog : PopupMasterComponent
    {
        [Parameter]
        public int OrderId { get; set; }

        [Inject]
        public DatabaseService DatabaseService { get; set; }


        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                if (_price > 0)
                {
                    errormsg = string.Empty;
                }
                else
                {
                    errormsg = "Price must be above 0.";
                }
            }
        }

        private string errormsg = string.Empty;
        private void InvalidSubmission()
        {

        }

        private async Task OnSubmit()
        {
            if (Price > 0)
            {
                errormsg = string.Empty;
                var DetailedOrder = DatabaseService.GetOrderWithDetails(OrderId);
                if (DetailedOrder != null)
                {
                    DetailedOrder.IsByQty = DetailedOrder.SubCategoryName.ToLower().Contains("quantity");
                    DetailedOrder.IsByWeight = DetailedOrder.SubCategoryName.ToLower().Contains("weight");
                    DetailedOrder.Price = Price;

                    Order order = new Order()
                    {
                        Id = DetailedOrder.Id,
                        OrderNumber = DetailedOrder.OrderNumber,
                        SubCategoryId = DetailedOrder.SubCategoryId,
                        SellOrderId = DetailedOrder.SellOrderId,
                        PersonId = DetailedOrder.PersonId,
                        IsForSale = DetailedOrder.IsForSale,
                        Kg = DetailedOrder.Kg,
                        Quantity = DetailedOrder.Quantity,
                        IsByQty = DetailedOrder.IsByQty,
                        IsByWeight = DetailedOrder.IsByWeight,
                        Comission = DetailedOrder.Comission,
                        Fare = DetailedOrder.Fare,
                        Labour = DetailedOrder.Labour,
                        Price = DetailedOrder.Price,
                        CreatedOn = DetailedOrder.CreatedOn,
                    };
                    DatabaseService.UpdateOrder(order);
                    await ModalSubmit();
                }
            }
            else
            {
                errormsg = "Price must be above 0.";
                return;
            }
        }
    }
}
