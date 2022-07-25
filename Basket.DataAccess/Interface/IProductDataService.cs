using System.Collections.Generic;
using Basket.Models;

namespace Basket.DataAccess.Interface
{
    public interface IProductDataService
    {
        List<Game> GetAllProducts();

        decimal GetPrice(int productId);

        Game GetProduct(int productId);
    }
}
