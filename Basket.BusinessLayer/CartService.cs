using Basket.Models;
using System.Collections.Generic;
using System.Linq;

namespace Basket.BusinessLayer
{
    public class CartService : ICartService
    {
        private static Dictionary<string, ClientSessionInfo> _cart = new Dictionary<string, ClientSessionInfo>();

        private readonly IGamesService _gamesService;

        private readonly ISessionService _sessionService;

        private static object _lockObject = new object();

        public CartService()
        {
            _gamesService = new GamesService();

            _sessionService = new SessionService();
        }

        public void Add(int gameId, int quontity, string sessionId)
        {
            lock(_lockObject)
            {
                if (!_sessionService.CheckExpirationDate(sessionId))
                {
                    throw new System.Exception("Unknown sessionId");
                }

                bool validationResult = _sessionService.CheckExpirationDate(sessionId);

                if (!validationResult)
                {
                    _cart.Remove(sessionId);
                }
                else
                {
                    var game = _gamesService.GetProduct(gameId);

                    if (!_cart.ContainsKey(sessionId))
                    {
                        _cart[sessionId] = new ClientSessionInfo
                        {
                            Cart = new List<CartLine>()
                        };
                    };

                    var currentGame = _cart[sessionId].Cart.FirstOrDefault(t => t.GameId == gameId);

                    if (currentGame == null)
                    {
                        _cart[sessionId].Cart.Add(new CartLine
                        {
                            GameId = gameId,
                            Quantity = quontity,
                            Price = game.Price,
                            Name = game.Name
                        });
                    }
                    else
                    {
                        currentGame.Quantity += quontity;
                    }
                }
            }
        }

        public void Delete(int gameId, string sessionId)
        {
            if (!_sessionService.CheckExpirationDate(sessionId))
            {
                throw new System.Exception("Unknown sessionId");
            }
            
            if (!_cart.ContainsKey(sessionId))
            {
                _cart[sessionId] = new ClientSessionInfo
                {
                    Cart = new List<CartLine>()
                };
            }
            
            var currentGame = _cart[sessionId].Cart.FirstOrDefault(t => t.GameId == gameId);

            if (currentGame.Quantity > 1)
            {
                currentGame.Quantity--;

            }
            else
            {
                _cart[sessionId].Cart.RemoveAll(t => t.GameId == gameId);
            }
        }

        public List<CartLine> GetAllEntries(string sessionId)
        {
            if (!_sessionService.CheckExpirationDate(sessionId))
            {
                throw new System.Exception("Unknown sessionId");
            }

            if (!_cart.ContainsKey(sessionId))
            {
                return new List<CartLine>();
            }

            return _cart[sessionId].Cart;
        }

        public decimal OveralPrice(List<CartLine> cart)
        {
           decimal overalPrice = 0;

            if (cart != null)
            {
                foreach (var line in cart)
                {
                    var game = _gamesService.GetProduct(line.GameId);

                    var currentSum = game.Price * line.Quantity;

                    overalPrice += currentSum;
                }
            }

            return overalPrice;
        }

        public void RemoveAllCart(string sessionId)
        {
            if (_cart.ContainsKey(sessionId))
            {
                _cart.Remove(sessionId);
            }
        }

        public void AddClientPurchaseDetails(string sessionId, ClientDetails clientDetails)
        {
            if (!_sessionService.CheckExpirationDate(sessionId))
            {
                throw new System.Exception("Unknown sessionId");
            }

            if (!_cart.ContainsKey(sessionId))
            {
                _cart[sessionId] = new ClientSessionInfo
                {
                    Cart = new List<CartLine>()
                };
            }

            _cart[sessionId].PurchaseDetails = clientDetails;
        }

        public ClientDetails GetClientDetails(string sessionId)
        {
            if (!_sessionService.CheckExpirationDate(sessionId))
            {
                throw new System.Exception("Unknown sessionId");
            }

            if (!_cart.ContainsKey(sessionId))
            {
                _cart[sessionId] = new ClientSessionInfo
                {
                    Cart = new List<CartLine>()
                };
            }

            return _cart[sessionId].PurchaseDetails;
        }
    }
}
