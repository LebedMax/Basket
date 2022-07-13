using Basket.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Web.Helpers;
using Basket.BusinessLayer.Interface;

namespace GamesMarket.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        private readonly ISessionService _sessionService;

        public CartController()
        {
            _cartService = new CartService();

            _sessionService = new SessionService();
        }

        public JsonResult GetCart(string session)
        {
            var goodsBySession = _cartService.GetAllEntries(session);
            
            return Json(goodsBySession, JsonRequestBehavior.AllowGet);
        }

        public void Add(int gameId, int quontity, string sessionId)
        {
            _cartService.Add(gameId, quontity, sessionId);
        }

        public ActionResult CreateSession()
        {
            return Content(_sessionService.CreateNewSession());
        }

        public ActionResult CheckSession(string sessionId)
        {
            return Content(_sessionService.CheckExpirationDate(sessionId).ToString());
        }
    }
}