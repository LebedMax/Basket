using Basket.Models;
using Data_Layer;
using System;
using System.Data.SqlClient;

namespace Basket.Data_Layer
{
    public class OrderData : IOrderData
    {
        private string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=List_goods;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public void CreateNewOrder()
        {
            var connection = new SqlConnection();

            connection.ConnectionString = _connectionString;

            connection.Open();

            var command = new SqlCommand();

            command.Connection = connection;

            command.CommandText = "INSERT INTO [dbo].[Order] ([id], [name], [surname], [adress],[phone],[email],[date],[idGameDetail]) VALUES (1, N'Max', N'Lebed', N'Вулиця Степана Бандери 1',N'380983672711',N'111@gmail.com',N'2012-12-22',1)";

           // command.Parameters.Add(new SqlParameter("@idParam", id)); 

            command.ExecuteNonQuery();

            connection.Close();
        }

        public Order GetOrderById(int id)
        {
            var result = new Order();

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                var command = conn.CreateCommand();

                command.CommandText = "SELECT * FROM [dbo].[Table] WHERE [id] = @idParam";

                command.Parameters.Add(new SqlParameter("@idParam", id));

                conn.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = new Order
                    {
                        name = reader["name"].ToString(),
                        surname = reader["surname"].ToString(),
                        date = Convert.ToDateTime(reader["date"].ToString())
                    };
                }
            }

            return result;
        }
    }
    }

