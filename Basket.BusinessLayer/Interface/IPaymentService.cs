using Basket.Models;

namespace Basket.BusinessLayer.Interface
{
    public interface IPaymentService
    {
        void Pay(PaymentCard card, decimal totalPrice);
    }
}