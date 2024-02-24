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

        public string OrderNumber { get; set; }

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
                CalculateWeight();
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

        private double? _comission;
        public double? Comission
        {
            get { return _comission; }
            set
            {
                _comission = value;
                CalculateTotalAmont();
            }
        }

        private double? _fare;
        public double? Fare
        {
            get { return _fare; }
            set
            {
                _fare = value;
                CalculateTotalAmont();
            }
        }

        public double TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CalculateTotalAmont()
        {
            TotalAmount = 0;
            if (IsByQty && Quantity != null)
            {
                TotalAmount = (double)(Quantity * Price);
            }
            else if (IsByWeight && Kg != null)
            {
                if (Price > 0)
                {
                    TotalAmount = Price / 20 * Kg.Value;
                }
            }

            if (Comission != null && Price > 0)
            {
                ComissionInRs = Price * (Comission.Value / 100);
                TotalAmount = TotalAmount + ComissionInRs.Value;
            }

            if (Labour != null)
            {
                TotalAmount = TotalAmount + (double)Labour;
            }
            TotalAmount = Math.Round(TotalAmount, 2);
        }

        public void CalculateWeight()
        {
            if (IsByQty)
            {
                if (IsPotato)
                {
                    Kg = Quantity * 50;
                }
                else if (IsGarlic)
                {
                    Kg = Quantity * 40;
                }
                else if (IsOnion)
                {
                    Kg = Quantity * 10;
                }
            }
        }

        [Ignore]
        public bool IsPotato { get; set; }
        [Ignore]
        public bool IsOnion { get; set; }
        [Ignore]
        public bool IsGarlic { get; set; }
        [Ignore]
        public bool IsByQty { get; set; }
        [Ignore]
        public bool IsByWeight { get; set; }
        [Ignore]
        public double? ComissionInRs { get; set; }
    }
}
