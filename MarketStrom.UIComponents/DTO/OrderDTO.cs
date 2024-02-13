using MarketStrom.UIComponents.Models;

namespace MarketStrom.UIComponents.DTO
{
    public class OrderDTO : Order
    {
        public string SubCategoryName { get; set; }
        public string PersonName { get; set; }
        public string CategoryName { get; set; }
    }
}
