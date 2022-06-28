using Basket.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Basket.DataAccess
{
    public class PurchaseDataService : IPurchaseDataService
    {
        private string ConnectionString => 
            System.Configuration.ConfigurationManager.ConnectionStrings["AppDBContext"].ConnectionString;

        public int MakePurchase(List<CartLine> cartItems, Buyer buyer)
        {
            var orderId = -1;

            using (var connection = new SqlConnection(ConnectionString))
            {
                var currentDateTime = DateTime.Now;

                connection.Open();

                var transaction = connection.BeginTransaction();

                var command = connection.CreateCommand();

                command.Transaction = transaction;

                CreateOrder(command, currentDateTime, buyer);

                orderId = GetOrderId(connection.CreateCommand(), transaction, currentDateTime);

                InsertGameDetails(cartItems, connection.CreateCommand(), orderId, transaction);

                transaction.Commit();
            }

            return orderId;
        }

        #region private
        private void InsertGameDetails(List<CartLine> cartItems, SqlCommand command, int orderId, SqlTransaction transaction)
        {
            foreach (var item in cartItems)
            {
                command.Transaction = transaction;

                command.CommandText = "INSERT INTO [dbo].[GameDatail] ([orderId], [gameid], [price], [quantity]) VALUES " +
                    "(@orderIdParam, @gameidParam, @priceParam, @quantityParam)";

                command.Parameters.Add(new SqlParameter("@orderIdParam", System.Data.SqlDbType.Int)).Value = orderId;

                command.Parameters.Add(new SqlParameter("@gameidParam", System.Data.SqlDbType.Int)).Value = item.GameId;

                command.Parameters.Add(new SqlParameter("@priceParam", System.Data.SqlDbType.Decimal)).Value = item.Price;

                command.Parameters.Add(new SqlParameter("@quantityParam", System.Data.SqlDbType.Int)).Value = item.Quantity;

                command.ExecuteNonQuery();
            }
        }

        private int GetOrderId(SqlCommand command, SqlTransaction transaction, DateTime currentDateTime)
        {
            command.Transaction = transaction;

            command.CommandText = "SELECT ID FROM [dbo].[Order] WHERE date = @dateParameter";

            command.Parameters.Add(new SqlParameter("@dateParameter", System.Data.SqlDbType.DateTime)).Value = currentDateTime;

            using (var reader = command.ExecuteReader())
            {
                reader.Read();

                return int.Parse(reader["ID"].ToString());
            }
        }

        private void CreateOrder(SqlCommand command, DateTime currentDateTime, Buyer buyer)
        {
            command.CommandText = "INSERT INTO [dbo].[Order] ([name], [surname], [adress], [phone], [date], [email]) VALUES " +
                "(@nameParameter, @surnameParameter, @adressParameter, @phoneParameter, @dateParameter, @emailParameter)";

            command.Parameters.Add(new SqlParameter("@nameParameter", System.Data.SqlDbType.VarChar)).Value = buyer.FirstName;

            command.Parameters.Add(new SqlParameter("@surnameParameter", System.Data.SqlDbType.VarChar)).Value = buyer.LastName;

            command.Parameters.Add(new SqlParameter("@adressParameter", System.Data.SqlDbType.VarChar)).Value = buyer.Address;

            command.Parameters.Add(new SqlParameter("@phoneParameter", System.Data.SqlDbType.VarChar)).Value = buyer.PhoneNumber;

            command.Parameters.Add(new SqlParameter("@dateParameter", System.Data.SqlDbType.DateTime)).Value = currentDateTime;

            command.Parameters.Add(new SqlParameter("@emailParameter", System.Data.SqlDbType.VarChar)).Value = "test@gmail.com";

            command.ExecuteNonQuery();
        }
        #endregion
    }
}
