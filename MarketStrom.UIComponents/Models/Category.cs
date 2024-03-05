using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MarketStrom.UIComponents.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double DefaultWeight { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<SubCategory> SubCategories { get; set; } = new();
    }
}
