using DealDouble.Entities;
using DealDoubleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class AuctionsController : Controller
    {
        // GET: Auctions
        public ActionResult Index()
        {
            AuctionsService service = new AuctionsService();
            var auctions = service.GetAllAuction();
            if (Request.IsAjaxRequest())
            {
                return PartialView(auctions);
            }
            else
            {
                return View(auctions);
            }
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Auction auction)
        {
            AuctionsService service = new AuctionsService();
            service.SaveAuction(auction);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            AuctionsService service = new AuctionsService();
            var auction = service.GetAuctionByID(ID);
            return PartialView(auction);
        }
        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
            AuctionsService service = new AuctionsService();
            service.UpdateAuction(auction);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int ID)
        {
            AuctionsService service = new AuctionsService();
            var auction = service.GetAuctionByID(ID);
            return PartialView(auction);
        }
        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
            
                AuctionsService service = new AuctionsService();
                service.DeleteAuction(auction);
                return RedirectToAction("Index");
            
         
        }

    }

}