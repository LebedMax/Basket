using Basket.BusinessLayer;
using Basket.Models;
using GamesMarket.Models;
using System;
using System.Web.Mvc;

namespace GamesMarket.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ICartService _cartService;

        private readonly ISessionService _sessionService;

        private readonly IPurchaseService _purchaseService;

        public PurchaseController()
        {
            _cartService = new CartService();

            _sessionService = new SessionService();

            _purchaseService = new PurchaseService();
        }

        public ActionResult Index(string session)
        {
            var basketProducts = _cartService.GetAllEntries(session);

            return View(basketProducts);
        }

        [HttpPost]
        public ActionResult MakePurchase(PurchaseItem item)
        {
            if (!_sessionService.CheckExpirationDate(item.SessionId))
            {
                throw new System.Exception("Unknown sessionId");
            }

            bool validationResult = _sessionService.CheckExpirationDate(item.SessionId);

            if (!validationResult)
            {
                _cartService.RemoveAllCart(item.SessionId);

                return View("~/Shared/Error", new ErrorDetails { ErrorMessage = "Unknown sessionId" });
            }

            _cartService.AddClientPurchaseDetails(item.SessionId, new ClientDetails
            {
                Address = item.Address,
                Email = item.Email,
                Name = item.Name,
                Phone = item.Phone,
                Surname = item.Surname
            });

            var totalSum = _cartService.OveralPrice(_cartService.GetAllEntries(item.SessionId));

            return View("PaymentForm", new PaymentSessionWithSum
            { 
                SessionId = item.SessionId,
                Sum = totalSum.ToString()
            });
        }

        [HttpPost]
        public ActionResult Pay(PaymentDetails info)
        {
            var clientDetails = _cartService.GetClientDetails(info.SessionId);

            var totalSum = _cartService.OveralPrice(_cartService.GetAllEntries(info.SessionId));

            var orderId = -1;

            try
            {
                orderId = _purchaseService.Purchase(_cartService.GetAllEntries(info.SessionId), new Buyer
                {
                    Address = clientDetails.Address,
                    FirstName = clientDetails.Name,
                    LastName = clientDetails.Surname,
                    PhoneNumber = clientDetails.Phone
                });
            }
            catch (Exception ex)
            {
                return View("../Shared/Error",
                    new ErrorDetails
                    {
                        ErrorMessage = "Unhandled exception has occured: " + ex.Message,
                    });
            }

            return View("PaymentSuccessfull", new PaymentSuccessInfo
            {
                Email = clientDetails.Email,
                OrderId = orderId,
                TotalSum = totalSum.ToString()
            }); ;
        }
    }
}