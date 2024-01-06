using MarketStrom.UIComponents.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace MarketStrom.UIComponents.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _db;
        private bool _isModified;

        public DatabaseService()
        {

        }


        public bool Load(string filepath)
        {
            _db = new SQLiteConnection(filepath);

            if (!File.Exists(filepath))
            {
                CreateTables();
            }
            return true;
        }

        private void CreateTables()
        {
            _db.CreateTable<Person>();
            _db.CreateTable<Category>();
            _db.CreateTable<Order>();
            _db.CreateTable<SubCategory>();
        }

        #region Person

        public void InsertPerson(Person person)
        {
            _db.Insert(person);
        }

        public Person GetPerson(int id)
        {
            return _db.Get<Person>(id);
        }

        public void UpdatePerson(Person person)
        {
            _db.Update(person);
        }

        #endregion Person

        #region Category

        public void InsertCategory(Category category)
        {
            _db.Insert(category);
        }

        public void UpdateCategory(Category category)
        {
            _db.Update(category);
        }

        #endregion Category

        #region SubCategory

        public void InsertSubCategory(SubCategory subCategory)
        {
            _db.Insert(subCategory);
        }
        public void UpdateSubCategory(SubCategory subCategory)
        {
            _db.Update(subCategory);
        }

        #endregion SubCategory

        public List<Category>? GetAllCategory()
        {
            return _db.GetAllWithChildren<Category>().ToList();
        }

        public void DeleteRecord(int id, string table)
        {
            _db.Execute($"UPDATE {table} SET IsDeleted = 1 WHERE Id ={id};"); //SOFT DELETE RECORD
        }

        public List<Person> GetAllPerson()
        {
            return _db.GetAllWithChildren<Person>().ToList();
        }

        public void Dispose()
        {
            _db.Close();
            _db.Dispose();
        }
    }
}
