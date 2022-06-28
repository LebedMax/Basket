using System.Collections.Generic;

namespace Basket.Models
{
    public class ClientSessionInfo
    {
        public List<CartLine> Cart { get; set; }

        public ClientDetails PurchaseDetails { get; set; }
    }
}
