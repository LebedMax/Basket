using Basket.Models;

namespace Basket.DataAccess.Interface
{
    public interface IOrderDataService
    {
        void CreateNewOrder();
        
        Order GetOrderById(int id);
    }
}
