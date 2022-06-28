namespace Basket.Models
{
    public class CartLine
    {
        public int GameId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
