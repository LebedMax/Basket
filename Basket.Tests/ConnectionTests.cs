using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Basket.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckDbConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["GameMarketConnection"].ConnectionString;
            
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "SELECT CURRENT_TIMESTAMP";
                
                connection.Open();

                var reader = command.ExecuteReader();

                reader.Read();
                
                Console.WriteLine(reader[0]);
            }
        }
    }
}
