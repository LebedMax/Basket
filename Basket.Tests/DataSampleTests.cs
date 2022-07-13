using System;
using Basket.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Basket.Tests
{
    [TestClass]
    public class DataSampleTests
    {
        [TestMethod]
        public void GenerateBuyers()
        {
            var service = new DataGenerator();

            var buyers = service.GenerateBuyers(10);
            
            Console.WriteLine(JsonConvert.SerializeObject(buyers));
        }
        
        [TestMethod]
        public void GenerateProducts()
        {
            var service = new DataGenerator();

            var products = service.GenerateProducts(10);
            
            Console.WriteLine(JsonConvert.SerializeObject(products));
        }
    }
}
