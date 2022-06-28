using Basket.Models;
using System.Collections.Generic;

namespace Basket.DataAccess
{
    public interface IPurchaseDataService
    {
        int MakePurchase(List<CartLine> cart, Buyer buyer);
    }
}
