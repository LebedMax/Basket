using Basket.DataAccess;
using Basket.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Basket.BusinessLayer;
using System.Text;
using System.Threading.Tasks;

namespace Basket.BusinessLayer
{
    public class GamesService : IGamesService
    {
      //  private readonly List<Game> _games = new List<Game>();
        private string _connectionString =
           "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=List_goods;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
       
        //public GamesService()
        //{
        //    _games = new List<Game>
        //        {
        //            new Game
        //            {
        //                GameId = 1,
        //                Category="10",
        //                Description= "Minecraft",
        //                Name = "wrgwtew",
        //                Price = 100
        //            },
        //            new Game
        //            {
        //                GameId = 2,
        //                Category="10",
        //                Description= "thbsrbs",
        //                Name = "thgwefsd",
        //                Price = 130
        //            },
        //            new Game
        //            {
        //                GameId = 3,
        //                Category="10",
        //                Description= "trewqsd",
        //                Name = "22123",
        //                Price = 110
        //            }
        //        };
        //}

        public List<Game> AllItems()
        {
            var result = new List<Game>();

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

            var command = conn.CreateCommand();

            command.CommandText = "SELECT * FROM [dbo].[Games]";

            conn.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                    result.Add(new Game
                    {
                        GameId = Convert.ToInt32(reader["GameID"].ToString()),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"].ToString())
                    }); ;
            }
            return result;
        }
        //  return _games;
    }

        public decimal GetPrice(int productId)
        {
            //if (!_games.Any(t => t.GameId == productId))
            //{
            //    throw new Exception($"there is no game with {productId} id");
            //}

            //return _games.First(t => t.GameId == productId).Price;
            
       
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppDBContext"].ConnectionString;
            ;

          using (var context = new AppDBContext(connectionString))
          {
               var product = context.Games.FirstOrDefault(t => t.GameId == productId);

                if (product  == null)
               {
                
                    throw new Exception("No product found");       
               }

                return product.Price;
            }
        }

        public Game GetProduct(int productId)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppDBContext"].ConnectionString;

            using (var context = new AppDBContext(connectionString))
            {
                return context.Games.FirstOrDefault(t => t.GameId == productId);
            }
           // return _games.FirstOrDefault(t => t.GameId == productId);
        }
    }
    
}


//}
//private AppDBContext context = new AppDBContext();
//public GamesService()
//{
//    if(!context.Games.Any())
//    {
//        context = new List<Game>(
//            new Game

//            {
//                GameId = 1,
//                Category = "10",
//                Description = "2332432",
//                Name = "wrgwtew",
//                Price = 100
//            },
//            new Game
//            {
//                GameId = 2,
//                Category = "10",
//                Description = "thbsrbs",
//                Name = "thgwefsd",
//                Price = 130
//            },
//            new Game
//            {
//                GameId = 3,
//                Category = "10",
//                Description = "trewqsd",
//                Name = "22123",
//                Price = 110
//            })

//    }
//}

//    public game getproduct(int productid)
//    {
//        return _games.firstordefault(t => t.gameid == productid);
//    }
//}
//public decimal GetPrice(int productId)
//{
//    throw new NotImplementedException();
//}

//public Game GetProduct(int productId)
//{
//    throw new NotImplementedException();
//}

