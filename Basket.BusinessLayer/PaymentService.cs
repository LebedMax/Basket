using Basket.Models;
using System.Threading;
using Basket.BusinessLayer.Interface;

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
