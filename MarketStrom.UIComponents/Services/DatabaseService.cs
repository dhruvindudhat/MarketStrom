using Blazorise;
using MarketStrom.UIComponents.DTO;
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
            bool databaseExists = File.Exists(filepath);
            _db = new SQLiteConnection(filepath);

            if (!databaseExists)
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
            _db.CreateTable<Login>();
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
            _db.InsertWithChildren(category, true);
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                _db.InsertOrReplaceWithChildren(category, true);
            }
            catch (Exception e)
            {

            }
        }

        public Category GetCategory(int id)
        {
            return _db.GetWithChildren<Category>(id);
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

        public List<Category> GetAllCategory()
        {
            return _db.GetAllWithChildren<Category>().ToList();
        }

        public void DeleteCategory(Category category)
        {
            _db.Delete(category, true);
        }

        #endregion SubCategory

        #region Order

        public List<OrderDTO> GetAllOrders()
        {
            string query = $"SELECT `Order`.*, SubCategory.Name AS SubCategoryName, Person.FirstName || ' ' || Person.LastName AS PersonName FROM" + "`Order` INNER JOIN SubCategory  ON `Order`.SubCategoryId = SubCategory.Id INNER JOIN Person ON Person.Id = `Order`.PersonId;";
            return _db.Query<OrderDTO>(query);
        }

        public void InsertOrder(Order order)
        {
            _db.Insert(order);
        }

        public void UpdateOrder(Order order)
        {
            _db.Update(order);
        }

        public Order GetOrder(int id)
        {
            return _db.GetWithChildren<Order>(id);
        }

        #endregion

        public void DeleteRecord(int id, string table)
        {
            _db.Execute($"UPDATE {table} SET IsDeleted = 1 WHERE Id ={id};"); //SOFT DELETE RECORD
        }

        public List<Person> GetAllPerson()
        {
            return _db.GetAllWithChildren<Person>().Where(o => o.IsDeleted == false).ToList();
        }

        public void SaveUser(Login loginData)
        {
            _db.Insert(loginData);
        }

        public Login GetUser(string username)
        {
            return _db.Table<Login>().FirstOrDefault(x => x.Username == username);
        }

        public void Dispose()
        {
            _db.Close();
            _db.Dispose();
        }
    }
}
