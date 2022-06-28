using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Models
{
    public class GameDetail
    {
        public int id { set; get; }
        public int orderId { set; get; }
        public int gameId { set; get; }
        public uint price { set; get; }
        public virtual Game game { set; get; }

        public virtual Order order { set; get; }
    }
}
