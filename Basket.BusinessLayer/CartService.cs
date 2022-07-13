using Basket.Models;
using System.Collections.Generic;
using System.Linq;
using Basket.BusinessLayer.Interface;

namespace Basket.BusinessLayer
{
    public class CartService : ICartService
    {
        private static readonly Dictionary<string, ClientSessionInfo> Cart =
            new Dictionary<string, ClientSessionInfo>();

        private readonly IGamesService _gamesService;

        private readonly ISessionService _sessionService;

        private static readonly object LockObject = new object();

        public CartService()
        {
            _gamesService = new GamesService();

            _sessionService = new SessionService();
        }

        public void Add(int gameId, int quantity, string sessionId)
        {
            lock(LockObject)
            {
                if (!_sessionService.CheckExpirationDate(sessionId))
                {
                    throw new System.Exception("Unknown sessionId");
                }

                var validationResult = _sessionService.CheckExpirationDate(sessionId);

                if (!validationResult)
                {
                    Cart.Remove(sessionId);
                }
                else
                {
                    var game = _gamesService.GetProduct(gameId);

                    if (!Cart.ContainsKey(sessionId))
                    {
                        Cart[sessionId] = new ClientSessionInfo
                        {
                            Cart = new List<CartLine>()
                        };
                    }

                    var currentGame = Cart[sessionId].Cart.FirstOrDefault(t => t.GameId == gameId);

                    if (currentGame == null)
                    {
                        Cart[sessionId].Cart.Add(new CartLine
                        {
                            GameId = gameId,
                            Quantity = quantity,
                            Price = game.Price,
                            Name = game.Name
                        });
                    }
                    else
                    {
                        currentGame.Quantity += quantity;
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
            
            if (!Cart.ContainsKey(sessionId))
            {
                Cart[sessionId] = new ClientSessionInfo
                {
                    Cart = new List<CartLine>()
                };
            }
            
            var currentGame = Cart[sessionId].Cart.FirstOrDefault(t => t.GameId == gameId);

            if (currentGame == null)
            {
                return;
            }

            if (currentGame.Quantity > 1)
            {
                currentGame.Quantity--;
            }
            else
            {
                Cart[sessionId].Cart.RemoveAll(t => t.GameId == gameId);
            }
        }

        public List<CartLine> GetAllEntries(string sessionId)
        {
            if (!_sessionService.CheckExpirationDate(sessionId))
            {
                throw new System.Exception("Unknown sessionId");
            }

            return !Cart.ContainsKey(sessionId)
                ? new List<CartLine>()
                : Cart[sessionId].Cart;
        }

        public decimal OverallPrice(List<CartLine> cart)
        {
           decimal overallPrice = 0;

           if (cart == null)
           {
               return overallPrice;
           }
           
           foreach (var line in cart)
           {
                var game = _gamesService.GetProduct(line.GameId);

                var currentSum = game.Price * line.Quantity;

                overallPrice += currentSum;
           }

           return overallPrice;
        }

        public void RemoveAllCart(string sessionId)
        {
            if (Cart.ContainsKey(sessionId))
            {
                Cart.Remove(sessionId);
            }
        }

        public void AddClientPurchaseDetails(string sessionId, ClientDetails clientDetails)
        {
            if (!_sessionService.CheckExpirationDate(sessionId))
            {
                throw new System.Exception("Unknown sessionId");
            }

            if (!Cart.ContainsKey(sessionId))
            {
                Cart[sessionId] = new ClientSessionInfo
                {
                    Cart = new List<CartLine>()
                };
            }

            Cart[sessionId].PurchaseDetails = clientDetails;
        }

        public ClientDetails GetClientDetails(string sessionId)
        {
            if (!_sessionService.CheckExpirationDate(sessionId))
            {
                throw new System.Exception("Unknown sessionId");
            }

            if (!Cart.ContainsKey(sessionId))
            {
                Cart[sessionId] = new ClientSessionInfo
                {
                    Cart = new List<CartLine>()
                };
            }

            return Cart[sessionId].PurchaseDetails;
        }
    }
}
