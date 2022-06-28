using Basket.Models;
using System.Collections.Generic;

namespace Basket.BusinessLayer
{
    public interface IGamesService
    {
        decimal GetPrice(int productId);

        Game GetProduct(int productId);

        List<Game> AllItems();
    }
}
