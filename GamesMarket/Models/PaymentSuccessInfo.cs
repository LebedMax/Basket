namespace GamesMarket.Models
{
    public class PaymentSuccessInfo
    {
        public int OrderId { get; set; }

        public string Email { get; set; }

        public string TotalSum { get; set; }
    }
}