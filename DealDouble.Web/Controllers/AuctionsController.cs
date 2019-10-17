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
        AuctionsService service = new AuctionsService();
        // GET: Auctions
        public ActionResult Index()
        {
          
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
            
            service.SaveAuction(auction);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            
            var auction = service.GetAuctionByID(ID);
            return PartialView(auction);
        }
        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
           
            service.UpdateAuction(auction);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int ID)
        {
            
            var auction = service.GetAuctionByID(ID);
            return PartialView(auction);
        }
        [HttpPost]
        public ActionResult Delete(Auction auction)
        {

                service.DeleteAuction(auction);
                return RedirectToAction("Index");
            
        }

        public ActionResult Details(int ID)
        { 
            var auction = service.GetAuctionByID(ID);
            return View(auction);
        }

    }

}