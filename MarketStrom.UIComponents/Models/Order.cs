using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MarketStrom.UIComponents.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(SubCategory))]
        public int SubCategoryId { get; set; }

        [ForeignKey(typeof(Person))]
        public int PersonId { get; set; }
        public int? Quantity { get; set; }
        public double? Kg { get; set; }
        public double Price { get; set; }
        public int? Labour { get; set; }
        public double TotalAmount { get; set; }
    }
}
