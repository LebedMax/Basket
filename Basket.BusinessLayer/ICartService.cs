using System.Collections.Generic;
using Basket.Models;

namespace Basket.BusinessLayer
{
    public interface ICartService
    {
        void Add(int gameId, int quontity, string sessionId);

        void Delete(int gameId, string sessionId);

        List<CartLine> GetAllEntries(string sessionId);

        decimal OveralPrice(List<CartLine> cart);

        void RemoveAllCart(string sessionId);

        void AddClientPurchaseDetails(string sessionId, ClientDetails clientDetails);

        ClientDetails GetClientDetails(string sessionId);
    }
}
