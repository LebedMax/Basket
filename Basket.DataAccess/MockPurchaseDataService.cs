using Basket.Models;
using System;
using System.Collections.Generic;
using Basket.DataAccess.Interface;
using Bogus;

namespace Basket.DataAccess
{
    public class MockPurchaseDataService : IPurchaseDataService
    {
        public int MakePurchase(List<CartLine> cartItems, Buyer buyer)
        {
            return Randomizer.Seed.Next(DateTime.Now.Millisecond);
        }
    }
}
