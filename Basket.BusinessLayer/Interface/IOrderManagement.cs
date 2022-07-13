using System;

namespace Basket.BusinessLayer.Interface
{
    public interface IOrderManagement
    {
        void CreateNewOrder(string name, string surname, DateTime date);

    }
}
