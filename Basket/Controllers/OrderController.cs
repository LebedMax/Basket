using Basket.BusinessLayer;
using System.Web.Mvc;
using Basket.Models;
using System.Web.Http;
// using Basket.BusinessLayer.Interface;

namespace Basket.Controllers
{
    public class OrderController : Controller
    {
        // private IOrderManagement _orderManagement;
        // private readonly IOrderData _OrderData;
        
        public OrderController()
        {
            // _orderManagement = new OrderManagement();
        }

        public void CreateNewOrder([FromBody] Order order)
        {
            // _orderManagement.CreateNewOrder(order.name, order.surname, order.date);
        }
        // public ActionResult Index()
        // {
        //     return View();
        // }
    }
}