using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace MarketStrom.UIComponents.Services
{
    public class DatabaseService
    {
        private SQLiteConnection _db;

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
            _db.CreateTable<PaymentHistory>();
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
            _db.InsertOrReplaceWithChildren(category, true);
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
            string query = $"SELECT `Order`.*, SubCategory.Name AS SubCategoryName, Category.Name AS CategoryName,Person.FirstName || ' ' || Person.LastName AS PersonName FROM `Order` INNER JOIN SubCategory  ON `Order`.SubCategoryId = SubCategory.Id INNER JOIN Category  ON Category.Id = SubCategory.CategoryId LEFT JOIN Person ON Person.Id = `Order`.PersonId WHERE Person.Role = 4;";
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

        public void DeleteOrder(int orderId)
        {
            _db.Delete<Order>(orderId);
        }

        public Order GetOrder(int id)
        {
            return _db.GetWithChildren<Order>(id);
        }

        public OrderDTO GetOrderWithDetails(int id)
        {
            string query = $"Select o.*,sc.name as SubCategoryName FROM `Order` o INNER JOIN SubCategory sc ON sc.Id = o.SubCategoryId WHERE O.Id =" + id;
            return _db.Query<OrderDTO>(query).FirstOrDefault();
        }

        public int GetLastOrderNumber()
        {
            string query = $"SELECT id From `Order` ORDER BY Id DESC LIMIT 1;";
            return _db.ExecuteScalar<int>(query);
        }

        public void SetOrderStatusPaid(int orderId, PaymentStatus status)
        {
            _db.Execute($"UPDATE `Order` SET PaymentStatus = {(int)status} WHERE Id = {orderId}");
        }
        #endregion

        #region User

        public void SaveUser(Login loginData)
        {
            _db.Insert(loginData);
        }

        public Login GetUser(string username)
        {
            return _db.Table<Login>().FirstOrDefault(x => x.Username == username);
        }


        #endregion User

        #region DashBoard

        public List<OrderDTO> GetAvailableOrders()
        {
            string query = $"SELECT o.Id, o.SubCategoryId,o.IsForSale,sc.Name AS SubCategoryName, c.name AS CategoryName,SUM(s.Quantity) AS SoldQuantity,o.Quantity,o.Kg,SUM(s.Kg) AS SoldWeight FROM `Order` o LEFT JOIN `Order` s ON s.SellOrderId = o.Id INNER JOIN SubCategory sc ON sc.Id = o.SubCategoryId INNER JOIN Category c ON c.Id = sc.CategoryId WHERE o.SellOrderId IS NULL GROUP BY o.Id, o.SubCategoryId, o.Price Having SUM(s.Quantity) IS NULL OR SUM(s.Quantity) *(-1) != o.Quantity;";
            return _db.Query<OrderDTO>(query);
        }

        #endregion


        #region Payment

        public void InsertPaymentOrder(PaymentHistory payment)
        {
            _db.Insert(payment);
        }

        public List<PaymentHistory> GetAllWithoutFullPaymentOrders(int personId)
        {
            string query = $"SELECT * FROM PaymentHistory WHERE PaymentHistory.PersonId =" + personId + " AND PaymentHistory.IsFullPaymentCompleted = false;";
            return _db.Query<PaymentHistory>(query);
        }

        public void UpdatePaymentOrder(PaymentHistory paymentOrder)
        {
            _db.Update(paymentOrder);
        }

        public List<PaymentHistory> GetAllPaymentOrders(int personId)
        {
            string query = $"SELECT * FROM PaymentHistory WHERE PaymentHistory.PersonId =" + personId;
            return _db.Query<PaymentHistory>(query);
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

        public List<OrderDTO> GetAllPendingSellOrderByPerson(int personId)
        {
            string query = $"SELECT `Order`.Id,`Order`.SubCategoryId,`Order`.SellOrderId,`Order`.OrderNumber,`Order`.IsForSale,`Order`.Price,(`Order`.Quantity * -1) AS Quantity, (`Order`.Kg * -1) AS Kg,`Order`.Labour,`Order`.Comission,`Order`.Fare,(`Order`.TotalAmount * -1) As TotalAmount,`Order`.ComissionAmount,`Order`.LabourAmount,`Order`.CreatedOn, SubCategory.Name AS SubCategoryName, Category.Name AS CategoryName,Person.FirstName || ' ' || Person.LastName AS PersonName FROM `Order` INNER JOIN SubCategory  ON `Order`.SubCategoryId = SubCategory.Id INNER JOIN Category  ON Category.Id = SubCategory.CategoryId LEFT JOIN Person ON Person.Id = `Order`.PersonId  where `Order`.SellOrderId IS NOT NULL AND `Order`.PaymentStatus = 0 AND `Order`.PersonId =" + personId;
            return _db.Query<OrderDTO>(query);
        }

        public List<OrderDTO> GetAllSellOrderByPerson(int personId)
        {
            string query = $"SELECT `Order`.Id,`Order`.SubCategoryId,`Order`.SellOrderId,`Order`.PaymentStatus,`Order`.OrderNumber,`Order`.IsForSale,`Order`.Price,(`Order`.Quantity * -1) AS Quantity, (`Order`.Kg * -1) AS Kg,`Order`.Labour,`Order`.Comission,`Order`.Fare,(`Order`.TotalAmount * -1) As TotalAmount,`Order`.ComissionAmount,`Order`.LabourAmount,`Order`.CreatedOn, SubCategory.Name AS SubCategoryName, Category.Name AS CategoryName,Person.FirstName || ' ' || Person.LastName AS PersonName FROM `Order` INNER JOIN SubCategory  ON `Order`.SubCategoryId = SubCategory.Id INNER JOIN Category  ON Category.Id = SubCategory.CategoryId LEFT JOIN Person ON Person.Id = `Order`.PersonId  where `Order`.SellOrderId IS NOT NULL AND `Order`.PersonId =" + personId;
            return _db.Query<OrderDTO>(query);
        }

        public void Dispose()
        {
            _db.Close();
            _db.Dispose();
        }
    }
}
