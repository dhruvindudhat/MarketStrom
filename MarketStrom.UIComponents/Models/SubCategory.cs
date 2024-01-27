using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MarketStrom.UIComponents.Models
{
    public class SubCategory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [ManyToOne("CategoryId", CascadeOperations = CascadeOperation.All)]
        public Category Category { get; set; }
    }
}
