using System.Collections.Generic;
using Basket.Models;

namespace Basket.BusinessLayer.Interface
{
    public interface ICartService
    {
        void Add(int gameId, int quantity, string sessionId);

        void Delete(int gameId, string sessionId);

        List<CartLine> GetAllEntries(string sessionId);

        decimal OverallPrice(List<CartLine> cart);

        void RemoveAllCart(string sessionId);

        void AddClientPurchaseDetails(string sessionId, ClientDetails clientDetails);

        ClientDetails GetClientDetails(string sessionId);
    }
}
