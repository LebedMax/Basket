using System;
using Basket.BusinessLayer.Interface;
using Basket.DataAccess;

namespace Basket.BusinessLayer
{
    public class OrderManagement : IOrderManagement
     
    {
        private readonly OrderDataService _dataLayer;
        public OrderManagement()
        {
            _dataLayer = new OrderDataService();
        }
        
        public void CreateNewOrder(string name, string surname, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ApplicationException("Name should contain value");
            }

            _dataLayer.CreateNewOrder();
        }
    }
}
