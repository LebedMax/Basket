using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Basket.DataAccess.Interface;
using Basket.Models;

namespace Basket.DataAccess;

public class ProductsDataService : IProductDataService
{
    private static string ConnectionString => 
        System.Configuration.ConfigurationManager.ConnectionStrings["GameMarketConnection"].ConnectionString;
    
    public List<Game> GetAllProducts()
    {
        var result = new List<Game>();
        
        using var connection = new SqlConnection(ConnectionString);

        connection.Open();
        
        var command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM PRODUCTS";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            result.Add(new Game
            {
                GameId = int.Parse(reader["ID"].ToString()),
                Description = reader["DESCRIPTION"].ToString(),
                Name = reader["NAME"].ToString(),
                Price = decimal.Parse(reader["PRICE"].ToString())
            });
        }
        
        return result;
    }

    public decimal GetPrice(int productId)
    {
        using var connection = new SqlConnection(ConnectionString);

        var command = connection.CreateCommand();

        command.CommandText = "SELECT PRICE FROM PRODUCTS WHERE ID = @productIdParameter";

        command.Parameters.Add("productIdParameter", SqlDbType.Int).Value = productId;
        
        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return decimal.Parse(reader["PRICE"].ToString());
        }
        
        throw new DataServiceException($"There is no product with id {productId}");
    }

    public Game GetProduct(int productId)
    {
        using var connection = new SqlConnection(ConnectionString);

        var command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM PRODUCTS WHERE ID = @productIdParameter";

        command.Parameters.Add("productIdParameter", SqlDbType.Int).Value = productId;
        
        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Game
            {
                GameId = int.Parse(reader["ID"].ToString()),
                Description = reader["DESCRIPTION"].ToString(),
                Name = reader["NAME"].ToString(),
                Price = decimal.Parse(reader["PRICE"].ToString())
            };
        }
        
        throw new DataServiceException($"There is no product with id {productId}");
    }
}