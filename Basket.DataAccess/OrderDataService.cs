using System.Data.SqlClient;
using Basket.DataAccess.Interface;
using Basket.Models;

namespace Basket.DataAccess
{
    public class OrderDataService : IOrderDataService
    {
        private static string ConnectionString => System.Configuration.ConfigurationManager
            .ConnectionStrings["GameMarketConnection"].ConnectionString;
        
        public void CreateNewOrder()
        {
            using var connection = new SqlConnection(ConnectionString);
            
            var command = connection.CreateCommand();

            command.CommandText = 
                "INSERT INTO Orders (NAME, SURNAME, ADRESS, PHONE, EMAIL, DATE, IDGAMEDETAIL) VALUES " +
                "(SUBSTR(MD5(RAND()), 1, 10), SUBSTR(MD5(RAND()), 1, 10), 'Kyiv, Zalisna', '+380931234567', " +
                "'someemail@somehost.com', CURRENT_DATE, 1)";
                
            connection.Open();

            var reader = command.ExecuteReader();

            var tet = reader[0].ToString();
                
            // command.ExecuteNonQuery();
        }
        
        public Order GetOrderById(int id)
        {
            var result = new Order();

            // using (var connection = new MySqlConnection(_connectionString))
            // {
            //     var command = connection.CreateCommand();
            //
            //     command.CommandText = "SELECT * FROM [dbo].[Table] WHERE [id] = @idParam";
            //
            //     command.Parameters.Add(new SqlParameter("@idParam", id));
            //
            //     connection.Open();
            //
            //     var reader = command.ExecuteReader();
            //
            //     if (reader.Read())
            //     {
            //         result = new Order
            //         {
            //             name = reader["name"].ToString(),
            //             surname = reader["surname"].ToString(),
            //             date = Convert.ToDateTime(reader["date"].ToString())
            //         };
            //     }
            // }

            return result;
        }
    }
    }

