using DealDouble.Web.ViewModels;
using DealDoubleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class HomeController : Controller
    {
        AuctionsService service = new AuctionsService();
        public ActionResult Index()
        {
            AuctionViewModel vModel = new AuctionViewModel();

            vModel.PageTitle = "Home Page";
            vModel.PageDescription = "I am home page";

            vModel.AllAuctions = service.GetAllAuction();
            vModel.PromotedAuctions = service.GetPromotedAuction();

           
            return View(vModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your home application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}