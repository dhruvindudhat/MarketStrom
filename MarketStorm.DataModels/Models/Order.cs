using MarketStrom.DataModels.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MarketStrom.DataModels.Models
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

        public int? SellOrderId { get; set; }

        public string OrderNumber { get; set; }

        private bool _isForSale;
        public bool IsForSale
        {
            get { return _isForSale; }
            set
            {
                _isForSale = value;
                CalculateTotalAmount();
            }
        }

        private double _price;
        [Required]
        //[Range(0.1, double.MaxValue, ErrorMessage = "Price must be above 0.")]
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                CalculateTotalAmount();
            }
        }

        private int? _quantity;
        public int? Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                CalculateTotalAmount();
            }
        }

        private double? _kg;
        public double? Kg
        {
            get { return _kg; }
            set
            {
                _kg = value;
                CalculateTotalAmount();
            }
        }

        private double? _labour;
        public double? Labour
        {
            get { return _labour; }
            set
            {
                _labour = value;
                CalculateTotalAmount();
            }
        }

        private double? _comission;
        public double? Comission
        {
            get { return _comission; }
            set
            {
                _comission = value;
                CalculateTotalAmount();
            }
        }

        private double? _fare;
        public double? Fare
        {
            get { return _fare; }
            set
            {
                _fare = value;
                CalculateTotalAmount();
            }
        }

        public double TotalAmount { get; set; }

        public double? ComissionAmount { get; set; }
        public double? LabourAmount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CalculateTotalAmount()
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
                ComissionAmount = Math.Round(TotalAmount * (Comission.Value / 100), 2);  //TotalAmount Percentage
                if (IsForSale && SellOrderId == null)
                    TotalAmount = TotalAmount - ComissionAmount.Value;
                else
                    TotalAmount = TotalAmount + ComissionAmount.Value;
            }
            else
            {
                ComissionAmount = null;
            }

            if (Labour != null && Quantity != null)
            {
                LabourAmount = Math.Round(Labour.Value * Quantity.Value, 2);
                if (IsForSale && SellOrderId == null)
                    TotalAmount = TotalAmount - LabourAmount.Value;
                else
                    TotalAmount = TotalAmount + LabourAmount.Value;
            }
            else
            {
                LabourAmount = null;
            }
            if (Fare != null)
            {
                if (IsForSale && SellOrderId == null)
                    TotalAmount = TotalAmount - Fare.Value;
                else
                    TotalAmount = TotalAmount + Fare.Value;
            }

            TotalAmount = Math.Round(TotalAmount, 0);
        }

        [Ignore]
        public bool IsByQty { get; set; }
        [Ignore]
        public bool IsByWeight { get; set; }
    }
}
