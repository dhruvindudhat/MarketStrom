using MarketStrom.DataModels.Models;

namespace MarketStrom.DataModels.DTO
{
    public class OrderDTO : Order
    {
        public string SubCategoryName { get; set; }
        public string PersonName { get; set; }
        public string CategoryName { get; set; }
        public double FinalTotal { get; set; }
        public int? SoldQuantity { get; set; }
        public double? SoldWeight { get; set; }
        public int? AvailableQuantity
        {
            get
            {
                if (SoldQuantity != null)
                    return Quantity + SoldQuantity;
                else return Quantity;
            }
        }
        public double? AvailableWeight
        {
            get
            {
                if (SoldWeight != null)
                    return Kg + SoldWeight;
                else return Kg;
            }
        }
    }
}
