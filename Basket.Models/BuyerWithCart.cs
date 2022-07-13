using System.Collections.Generic;

namespace Basket.Models
{
    public class BuyerWithCart
    {
        public Buyer Customer { get; set; }

        public List<CartLine> CartItems { get; set; }
    }
}