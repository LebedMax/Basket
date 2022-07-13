using System;
using System.Collections.Generic;

namespace Basket.Models
{
    public class Order
    {
        public int id { set; get; }
        public string name { set; get; }
        public string surname { set; get; }
        public string adress { set; get; }
        public string phone { set; get; }
        public string email { set; get; }
        public DateTime date { set; get; }

        public List<GameDetail> GameDatail { set; get; }

    }
}
