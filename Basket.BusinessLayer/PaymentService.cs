using Basket.Models;
using System.Threading;

namespace Basket.BusinessLayer
{
    public class PaymentService : IPaymentService
    {
        public void Pay(PaymentCard card, decimal totalPrice)
        {
            Thread.Sleep(1000);
        }
    }
}
