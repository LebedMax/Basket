using Basket.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Basket.DataAccess.Interface;

namespace Basket.DataAccess
{
    public class PurchaseDataService : IPurchaseDataService
    {
        private string ConnectionString => 
            System.Configuration.ConfigurationManager.ConnectionStrings["GameMarketConnection"].ConnectionString;

        public int MakePurchase(List<CartLine> cartItems, Buyer buyer)
        {
            using var connection = new SqlConnection(ConnectionString);

            connection.Open();

            var transaction = connection.BeginTransaction();
                
            var clientId = GetClientId(buyer, connection);

            var orderId = SaveOrder(clientId, connection);

            SaveOrderDetails(orderId, cartItems, connection);
            
            transaction.Commit();

            return orderId;
        }

        #region private
        private static void SaveOrderDetails(int orderId, List<CartLine> cartItems, SqlConnection connection)
        {
            foreach (var item in cartItems)
            {
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO ORDER_DETAILS (PRODUCTID, QUANTITY, PRICE, ORDERID) VALUES " +
                                      "(@productIdParameter, @quantityParameter, @priceParameter, @orderIdParameter)";
                
                command.Parameters.Add(new SqlParameter("productIdParameter", SqlDbType.Int)).Value = item.GameId;

                command.Parameters.Add(new SqlParameter("quantityParameter", SqlDbType.Int)).Value = item.Quantity;
                
                command.Parameters.Add(new SqlParameter("priceParameter", SqlDbType.Decimal)).Value = item.Price;
                
                command.Parameters.Add(new SqlParameter("orderIdParameter", SqlDbType.Int)).Value = orderId;

                command.ExecuteNonQuery();
            }
        }

        private static int SaveOrder(int clientId, SqlConnection connection)
        {
            var command = connection.CreateCommand();

            command.CommandText = "INSERT INTO ORDERS (CLIENTID, CREATEDDATE) VALUES " +
                                  "(@clientIdParameter, @createdDateParameter); " +
                                  "SELECT SCOPE_IDENTITY() ";

            command.Parameters.Add(new SqlParameter("clientIdParameter", SqlDbType.Int)).Value = clientId;

            command.Parameters.Add(new SqlParameter("createdDateParameter", SqlDbType.DateTime)).Value = DateTime.Now;

            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return int.Parse(reader["ID"].ToString());
            }

            throw new DataServiceException("Unable to save new order");
        }

        private static int GetClientId(Buyer buyer, SqlConnection connection)
        {
            var command = connection.CreateCommand();

            command.CommandText =
                "SELECT ID FROM CLIENTS " + 
                    "WHERE LOWER(FIRSTNAME) = @firstNameParameter " +
                    "AND LOWER(LASTNAME) = @lastNameParameter";

            command.Parameters.Add(new SqlParameter("firstNameParameter", SqlDbType.NVarChar)).Value =
                buyer.FirstName.ToLower();
            
            command.Parameters.Add(new SqlParameter("lastNameParameter", SqlDbType.NVarChar)).Value =
                buyer.LastName.ToLower();

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return int.Parse(reader["ID"].ToString());
                }   
            }

            command = connection.CreateCommand();
            
            command.CommandText =
                "INSERT INTO CLIENTS (FIRSTNAME, LASTNAME, PATRONYMIC, ADDRESS, PHONE) VALUES " +
                "(@firstNameParameter, @lastNameParameter, @patronymicParameter, @addressParamter, @phoneParameter); " + 
                "SELECT SCOPE_IDENTITY() ";
            
            command.Parameters.Add(new SqlParameter("firstNameParameter", SqlDbType.NVarChar)).Value =
                buyer.FirstName;
            
            command.Parameters.Add(new SqlParameter("lastNameParameter", SqlDbType.NVarChar)).Value =
                buyer.LastName;
            
            command.Parameters.Add(new SqlParameter("patronymicParameter", SqlDbType.NVarChar)).Value =
                buyer.Patronymic;
            
            command.Parameters.Add(new SqlParameter("addressParamter", SqlDbType.NVarChar)).Value =
                buyer.Address;
            
            command.Parameters.Add(new SqlParameter("phoneParameter", SqlDbType.NVarChar)).Value =
                buyer.PhoneNumber;

            var insertResult = command.ExecuteScalar();

            return int.Parse(insertResult.ToString());
        }
        #endregion
    }
}
