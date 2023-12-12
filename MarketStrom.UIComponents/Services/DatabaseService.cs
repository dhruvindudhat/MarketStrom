using MarketStrom.UIComponents.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace MarketStrom.UIComponents.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _db;
        private bool _isModified;

        public DatabaseService(string filename)
        {
            if (File.Exists(filename))
                Load(filename);
            else
                CreateNew(filename);

        }

        public bool CreateNew(string filepath)
        {
            _db = new SQLiteConnection(filepath);

            CreateTables();

            // Create root hiearchy
            //InsertHierarchy(new Hierarchy() { Name = Path.GetFileNameWithoutExtension(filepath), Level = HierarchyLevel.Root }, out _);

            return true;
        }

        public bool Load(string filepath)
        {
            _db = new SQLiteConnection(filepath);

            return true;
        }

        private void CreateTables()
        {
            _db.CreateTable<Person>();
            _db.CreateTable<Category>();
            _db.CreateTable<Order>();
            _db.CreateTable<SubCategory>();
        }

        public void InsertPerson(Person person)
        {
            _db.Insert(person);
        }

        public void InsertCategory(Category category)
        {
            _db.Insert(category);
        }
        public void InsertSubCategory(SubCategory subCategory)
        {
            _db.Insert(subCategory);
        }

        public List<Category>? GetAllCategory()
        {
            return _db.GetAllWithChildren<Category>().ToList();
        }


        public void Dispose()
        {
            _db.Close();
            _db.Dispose();
        }
    }
}
