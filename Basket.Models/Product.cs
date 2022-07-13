using System;

namespace Basket.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Picture { get; set; }

        public bool Availability { get; set; }
    }
}