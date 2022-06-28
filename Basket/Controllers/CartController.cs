using Basket.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Basket.Controllers
{
    public class CartController : ApiController
    {
        private readonly ISessionService _sessionService;

        private readonly ICartService _cartService;

        private readonly IGamesService _gamesService;

        public CartController()
        {
            _sessionService = new SessionService();

            _cartService = new CartService();

            _gamesService = new GamesService();
        }

        [HttpGet]
        public string StartNewSession()
        {
            return _sessionService.CreateNewSession();
        }

        [HttpPost]
        public void AddNewItem(int gameId, int quontity, string sessionId)
        {
            _cartService.Add(gameId, quontity, sessionId);
        }

        [HttpDelete]
        public void DeleteItem(int gameId, string sessionId)
        {
            _cartService.Delete(gameId, sessionId);
        }

        [HttpGet]
        public string GetAllEntries(string sessionId)
        {
            var sb = new StringBuilder();

            var currentCart = _cartService.GetAllEntries(sessionId);

            decimal overalPrice = 0;

            if (currentCart != null)
            {
                foreach (var line in currentCart)
                {
                    var game = _gamesService.GetProduct(line.GameId);

                    var currentSum = game.Price * line.Quantity;

                    overalPrice += currentSum;

                    sb.Append($"Game '{game.Name}', QTY: {line.Quantity} ({line.Price})   -  {currentSum}</br>");
                }
            }

            sb.Append($"Overal Price: {overalPrice}");

            return sb.ToString();
        }
    }
}