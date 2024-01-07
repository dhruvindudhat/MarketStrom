using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MarketStrom.UIComponents.Models
{
    public class Person
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        [RegularExpression(@"^(0|91|\+91)?-?[789]\d{9}$", ErrorMessage = "Invalid mobile number")]
        public string MobileNo { get; set; }
        public int Role { get; set; }
        [Required]
        public string City { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int IsDeletedBy { get; set; }
    }
}

