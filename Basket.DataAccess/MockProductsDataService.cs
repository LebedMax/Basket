using System.Collections.Generic;
using System.Linq;
using Basket.DataAccess.Interface;
using Basket.Models;

namespace Basket.DataAccess
{
    public class MockProductsDataService : IProductDataService
    {
        private static List<Game> _allGames;

        public MockProductsDataService()
        {
            _allGames = new DataGenerator().GenerateProducts(100)
                .Select(t =>
                    new Game
                    {
                        GameId = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        Price = t.Price
                    }
                ).ToList();
        }

        public List<Game> GetAllProducts()
        {
            return _allGames;
        }

        public decimal GetPrice(int productId)
        {
            var product = _allGames.FirstOrDefault(t => t.GameId == productId);

            return product?.Price ?? decimal.Zero;
        }

        public Game GetProduct(int productId)
        {
            return _allGames.FirstOrDefault(t => t.GameId == productId);
        }
    }
}