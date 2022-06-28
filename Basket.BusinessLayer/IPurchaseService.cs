using Basket.Models;
using System.Collections.Generic;

namespace Basket.BusinessLayer
{
    public interface IPurchaseService
    {
        int Purchase(List<CartLine> cart, Buyer buyer);
    }
}
