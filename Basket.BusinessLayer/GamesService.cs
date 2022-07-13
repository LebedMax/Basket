using Basket.DataAccess;
using Basket.Models;
using System.Collections.Generic;
using Basket.BusinessLayer.Interface;
using Basket.DataAccess.Interface;

namespace Basket.BusinessLayer
{
    public class GamesService : IGamesService
    {
        private readonly IProductDataService _productDataService;

        public GamesService()
        {
            _productDataService = new ProductsDataService();
        }
        
        public List<Game> AllItems()
        {
            return _productDataService.GetAllProducts();
        }

        public decimal GetPrice(int productId)
        {
            return _productDataService.GetPrice(productId);
        }

        public Game GetProduct(int productId)
        {
            return _productDataService.GetProduct(productId);
        }
    }
}
