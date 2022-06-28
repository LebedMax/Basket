
using Data_Layer;
using Basket.BusinessLayer;
using System.Web.Mvc;
using Basket.Models;
using System.Web.Http;

namespace Basket.Controllers
{
    public class OrderController : Controller
    {
        private IOrderManagement _orderManagement;

        public OrderController()
        {
            _orderManagement = new OrderManagement();
        }
        private readonly IOrderData _OrderData;
        public void CreateNewOrder([FromBody] Order order)
        {
            _orderManagement.CreateNewOrder(order.name, order.surname, order.date);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}