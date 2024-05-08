using MarketStrom.DataModels.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MarketStrom.DataModels.Models
{
    public class PaymentHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        [ForeignKey(typeof(Person))]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Person!!")]
        public int PersonId { get; set; }

        public double PaidAmount { get; set; }

        public string OrderIds { get; set; }

        public bool IsFullPaymentCompleted { get; set; }

        public DateTime ReceivedDate { get; set; }

        public PaymentMode PaymentMode { get; set; }
    }
}
