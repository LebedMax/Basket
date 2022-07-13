using Basket.DataAccess;
using Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basket.BusinessLayer.Interface;
using Basket.DataAccess.Interface;

namespace Basket.BusinessLayer
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPaymentService _paymentService;

        private readonly ICartService _cartService;

        private readonly IPurchaseDataService _purchaseDataService;

        public PurchaseService()
        {
            _paymentService = new PaymentService();

            _cartService = new CartService();

            _purchaseDataService = new PurchaseDataService();
        }

        public void Purchase(List<CartLine> cart, PaymentCard card, Buyer buyer)
        {
            var overallPrice = _cartService.OverallPrice(cart);

            ValidateCard(card);

            ValidateBuyer(buyer);

            _paymentService.Pay(card, overallPrice);
        }

        public int Purchase(List<CartLine> cart, Buyer buyer)
        {
            return _purchaseDataService.MakePurchase(cart, buyer);
        }

        private void ValidateBuyer(Buyer buyer)
        {
            //throw new ValidationException("dsfd");
          //  Regex regex =new Regex(@)

        }

        private void ValidateCard(PaymentCard card)
        {
            //throw new NotImplementedException();
        }
    }
}
