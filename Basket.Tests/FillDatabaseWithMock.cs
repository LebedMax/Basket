using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Basket.Models;
using Bogus;
using Bogus.DataSets;

namespace Basket.Tests
{
    [TestClass]
    public class FillDatabaseWithMock
    {
        [TestMethod]
        public void WipeDataFromDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["GameMarketConnection"].ConnectionString;
            
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();

                connection.Open();
                
                command.CommandText = "DELETE FROM ORDER_DETAILS";

                command.ExecuteNonQuery();

                command = connection.CreateCommand();
                
                command.CommandText = "DELETE FROM ORDERS";
                
                command.ExecuteNonQuery();
                
                command = connection.CreateCommand();
                
                command.CommandText = "DELETE FROM PRODUCTS";
                
                command.ExecuteNonQuery();
                
                command = connection.CreateCommand();
                
                command.CommandText = "DELETE FROM CLIENTS";
                
                command.ExecuteNonQuery();
            }
        }
        
        [TestMethod]
        public void FillMockDataToDatabase()
        {
            var clients = GenerateClients();

            var products = GenerateProducts();

            var orders = GenerateOrders(clients);

            var ordersDetails = GenerateOrdersDetails(orders, products);
            
            var connectionString = ConfigurationManager.ConnectionStrings["GameMarketConnection"].ConnectionString;
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                var transaction = connection.BeginTransaction();
               
                try
                {
                    FillProducts(products, connection, transaction);

                    FillClients(clients, connection, transaction);

                    FillOrders(orders, connection, transaction);
                
                    FillOrdersDetails(ordersDetails, connection, transaction);
                
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    
                    throw;
                }
            }
        }

        private static void FillOrdersDetails(List<OrderDetail> ordersDetails, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var orderDetail in ordersDetails)
            {
                var command = connection.CreateCommand();

                command.Transaction = transaction;
                
                command.CommandText = "INSERT INTO ORDER_DETAILS (ORDERID, PRODUCTID, QUANTITY, PRICE) " +
                                      "VALUES " +
                                      "(@orderIdParameter, @productIdParameter, @quantityParameter, @priceParameter)";

                command.Parameters.Add("orderIdParameter", SqlDbType.Int).Value = orderDetail.OrderId;
                    
                command.Parameters.Add("productIdParameter", SqlDbType.Int).Value = orderDetail.ProductId;
                    
                command.Parameters.Add("quantityParameter", SqlDbType.Int).Value = orderDetail.Quantity;
                    
                command.Parameters.Add("priceParameter", SqlDbType.Decimal).Value = orderDetail.Price;

                command.ExecuteNonQuery();
            }
        }

        private static void FillOrders(List<Order> orders, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var order in orders)
            {
                var command = connection.CreateCommand();

                command.Transaction = transaction;
                
                command.CommandText = "SET IDENTITY_INSERT ORDERS ON; " +
                                      "INSERT INTO ORDERS (ID, CLIENTID, CREATEDATE, STATUS, ADDRESS, PHONE, COMMENT) " +
                                      "VALUES " +
                                      "(@idParameter, @clientIdParameter, @createDateParameter, @statusParameter, " +
                                      "@addressParameter, @phoneParameter, @commentParameter)";

                command.Parameters.Add("idParameter", SqlDbType.Int).Value = order.Id;
                    
                command.Parameters.Add("clientIdParameter", SqlDbType.Int).Value = order.ClientId;
                    
                command.Parameters.Add("createDateParameter", SqlDbType.DateTime).Value = order.CreateDate;
                    
                command.Parameters.Add("statusParameter", SqlDbType.Int).Value = order.Status;
                    
                command.Parameters.Add("addressParameter", SqlDbType.NVarChar).Value = order.Address;
                    
                command.Parameters.Add("phoneParameter", SqlDbType.NVarChar).Value = order.Phone;
                
                command.Parameters.Add("commentParameter", SqlDbType.NVarChar).Value = order.Comment;

                command.ExecuteNonQuery();
            }
        }

        private static void FillClients(List<Client> clients, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var client in clients)
            {
                var command = connection.CreateCommand();

                command.Transaction = transaction;
                
                command.CommandText = "SET IDENTITY_INSERT CLIENTS ON; " +
                                      "INSERT INTO CLIENTS (ID, FIRSTNAME, LASTNAME, PATRONYMIC, ADDRESS, PHONE) " +
                                      "VALUES " +
                                      "(@idParameter, @firstNameParameter, @lastNameParameter, @patronymicParameter, " +
                                      "@addressParameter, @phoneParameter)";

                command.Parameters.Add("idParameter", SqlDbType.Int).Value = client.Id;
                    
                command.Parameters.Add("firstNameParameter", SqlDbType.NVarChar).Value = client.FirstName;
                    
                command.Parameters.Add("lastNameParameter", SqlDbType.NVarChar).Value = client.LastName;
                    
                command.Parameters.Add("patronymicParameter", SqlDbType.NVarChar).Value = client.Patronymic;
                    
                command.Parameters.Add("addressParameter", SqlDbType.NVarChar).Value = client.Address;
                    
                command.Parameters.Add("phoneParameter", SqlDbType.NVarChar).Value = client.PhoneNumber;

                command.ExecuteNonQuery();
            }
        }

        private static void FillProducts(List<Product> products, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var product in products)
            {
                var command = connection.CreateCommand();

                command.Transaction = transaction;

                command.CommandText = "SET IDENTITY_INSERT PRODUCTS ON; " +
                                      "INSERT INTO PRODUCTS (ID, NAME, DESCRIPTION, PRICE, CREATEDATE, MODIFYDATE, PICTURE, AVAILABILITY) " +
                                      "VALUES " +
                                      "(@idParameter, @nameParameter, @descriptionParameter, @priceParameter, " +
                                      "@createDateParameter, @modifyDateParameter, @picParameter, @availabilityParameter)";

                command.Parameters.Add("idParameter", SqlDbType.Int).Value = product.Id;
                    
                command.Parameters.Add("nameParameter", SqlDbType.NVarChar).Value = product.Name;
                    
                command.Parameters.Add("descriptionParameter", SqlDbType.NVarChar).Value = product.Description;
                    
                command.Parameters.Add("priceParameter", SqlDbType.Decimal).Value = product.Price;
                    
                command.Parameters.Add("createDateParameter", SqlDbType.DateTime).Value = product.CreateDate;
                    
                command.Parameters.Add("modifyDateParameter", SqlDbType.DateTime).Value = product.ModifyDate;
                    
                command.Parameters.Add("picParameter", SqlDbType.NVarChar).Value = product.Picture;
                    
                command.Parameters.Add("availabilityParameter", SqlDbType.Bit).Value = product.Availability;

                command.ExecuteNonQuery();
            }
        }

        private static List<OrderDetail> GenerateOrdersDetails(List<Order> orders, List<Product> products)
        {
            var result = new List<OrderDetail>();

            foreach (var order in orders)
            {
                var countOfItemsInOrder = Randomizer.Seed.Next(1, 5);
                
                for (var i = 0; i < countOfItemsInOrder; i++)
                {
                    var countOfProducts = Randomizer.Seed.Next(1, 5);
                    var productIndex = Randomizer.Seed.Next(0, products.Count);
                    var product = products[productIndex];
                    
                    result.Add(new OrderDetail
                    {
                        OrderId = order.Id,
                        Quantity = countOfProducts, 
                        Price = product.Price,
                        ProductId = product.Id
                    });
                }
            }

            return result;
        }

        private static List<Order> GenerateOrders(List<Client> clients)
        {
            var result = new List<Order>();

            var orderIndexer = 0;

            foreach (var client in clients)
            {
                var countOfOrders = Randomizer.Seed.Next(0, 5);

                for (var i = 0; i < countOfOrders; i++)
                {
                    result.Add(new Order
                    {
                        Id = ++orderIndexer,
                        Address = client.Address,
                        Phone = client.PhoneNumber,
                        Comment = new Faker().Lorem.Sentence(4),
                        Status = 0,
                        ClientId = client.Id,
                        CreateDate = new Faker().Date.Recent(10)
                    });
                }
            }

            return result;
        }

        private static List<Product> GenerateProducts()
        {
            Randomizer.Seed = new Random(DateTime.Now.Millisecond);
            
            var productGenerator = new Faker<Product>("uk")
                .RuleFor(o => o.Id, f => f.IndexGlobal)
                .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                .RuleFor(o => o.Description, f => f.Commerce.ProductDescription())
                .RuleFor(o => o.Picture, f => f.Image.PlaceImgUrl())
                .RuleFor(o => o.Availability, f => f.Random.Bool(0.9f))
                .RuleFor(o => o.Price, f => decimal.Parse(f.Commerce.Price(), NumberStyles.Currency))
                .RuleFor(o => o.CreateDate, f => f.Date.Recent(10))
                .RuleFor(o => o.ModifyDate, f => f.Date.Recent(3));

            return productGenerator.Generate(1000);
        }

        private List<Client> GenerateClients()
        {
            Randomizer.Seed = new Random(DateTime.Now.Millisecond);

            var clientGenerator = new Faker<Client>("uk")
                .RuleFor(o => o.Id, f => f.IndexGlobal)
                .RuleFor(o => o.Address, f => f.Address.FullAddress())
                .RuleFor(o => o.Patronymic, f => f.Name.FirstName(Name.Gender.Female))
                .RuleFor(o => o.FirstName, f => f.Name.FirstName(Name.Gender.Female))
                .RuleFor(o => o.LastName, f => f.Name.LastName(Name.Gender.Female))
                .RuleFor(o => o.PhoneNumber, f => $"0{Randomizer.Seed.Next(910000000, 999999999)}");

            return clientGenerator.Generate(500);
        }
    }

    public class Client
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Picture { get; set; }

        public bool Availability { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        
        public int ClientId { get; set; }

        public DateTime CreateDate { get; set; }

        public int Status { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Comment { get; set; }
    }

    public class OrderDetail
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
    
}
