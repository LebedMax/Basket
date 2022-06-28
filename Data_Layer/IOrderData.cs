using System.Collections.Generic;

using Basket.Models;

namespace Data_Layer
{
    public interface IOrderData
    {
        void CreateNewOrder();
        Order GetOrderById(int id);

    }
}
