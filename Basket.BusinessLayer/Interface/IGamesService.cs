using System.Collections.Generic;
using Basket.Models;

namespace Basket.BusinessLayer.Interface
{
    public interface IGamesService
    {
        decimal GetPrice(int productId);

        Game GetProduct(int productId);

        List<Game> AllItems();
    }
}
