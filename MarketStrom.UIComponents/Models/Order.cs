using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MarketStrom.UIComponents.Models
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        [ForeignKey(typeof(SubCategory))]
        public int SubCategoryId { get; set; }

        [Required]
        [ForeignKey(typeof(Person))]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Person!!")]
        public int PersonId { get; set; }


        private double _price;
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be above 0.")]
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                CalculateTotalAmont();
            }
        }

        private int? _quantity;
        public int? Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                CalculateTotalAmont();
            }
        }

        private double? _kg;
        public double? Kg
        {
            get { return _kg; }
            set
            {
                _kg = value;
                CalculateTotalAmont();
            }
        }

        private double? _labour;
        public double? Labour
        {
            get { return _labour; }
            set
            {
                _labour = value;
                CalculateTotalAmont();
            }
        }

        public double TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CalculateTotalAmont()
        {
            TotalAmount = 0;
            if (Kg != null)
            {
                TotalAmount = (double)(Kg * Price);
            }
            else if (Quantity != null)
            {
                TotalAmount = (double)(Quantity * Price);
            }

            if (Labour != null)
            {
                TotalAmount = TotalAmount + (double)Labour;
            }
            TotalAmount = Math.Round(TotalAmount, 2);
        }
    }
}
