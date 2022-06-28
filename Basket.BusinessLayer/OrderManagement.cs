using Basket.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.BusinessLayer
{
    public class OrderManagement : IOrderManagement
     
    {
        private OrderData _dataLayer;
        public OrderManagement()
        {
            _dataLayer = new OrderData();
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
