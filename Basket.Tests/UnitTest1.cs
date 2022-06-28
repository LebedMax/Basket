using Basket.BusinessLayer;
using Basket.DataAccess;
using Basket.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppDBContext"].ConnectionString;

            var service = new PurchaseDataService();

            service.MakePurchase(new List<CartLine>
            {
                new CartLine
                {
                    GameId = 1,
                    Price = 2.3M,
                    Quantity = 3
                },
                new CartLine
                {
                    GameId = 2,
                    Price = 5.3M,
                    Quantity = 2
                }
            }, new Buyer
            {
                Address = "qwerty",
                FirstName = "ASDF",
                LastName = "NBVC",
                PhoneNumber = "0912332"
            });
        }
    }
}
