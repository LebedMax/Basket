using Basket.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamesMarket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IGamesService _gamesService;

        public ProductsController()
        {
            _gamesService = new GamesService();
        }

        public ActionResult Index()
        {
            ViewBag.TestOne = "wertwerg";

            return View(_gamesService.AllItems());
        }
    }
}