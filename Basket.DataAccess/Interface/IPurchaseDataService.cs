using System.Collections.Generic;
using Basket.Models;

namespace Basket.DataAccess.Interface
{
    public interface IPurchaseDataService
    {
        int MakePurchase(List<CartLine> cart, Buyer buyer);
    }
}
