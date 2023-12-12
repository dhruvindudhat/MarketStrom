using SQLite;

namespace MarketStrom.UIComponents.Models
{
    public class Person
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string MobileNo { get; set; }
        public int Role { get; set; }
        public string City { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int IsDeletedBy { get; set; }
    }
}

