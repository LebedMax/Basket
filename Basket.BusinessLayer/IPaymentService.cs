using Basket.Models;

namespace Basket.BusinessLayer
{
    public interface IPaymentService
    {
        void Pay(PaymentCard card, decimal totalPrice);
    }
}