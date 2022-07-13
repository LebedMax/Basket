using System.Collections.Generic;
using Basket.Models;

namespace Basket.BusinessLayer.Interface
{
    public interface IPurchaseService
    {
        int Purchase(List<CartLine> cart, Buyer buyer);
    }
}
