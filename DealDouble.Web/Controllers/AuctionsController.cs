using DealDouble.Entities;
using DealDouble.Web.ViewModels;
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
            
            AuctionListingViewModel model = new AuctionListingViewModel();
            model.PageTitle = "Auctions";
            model.PageDescription = "Auction listing page";
            
                return View(model);
            
        }

        public ActionResult Listing()
        {
            AuctionListingViewModel model = new AuctionListingViewModel();
            model.Auctions = service.GetAllAuction();
            
                return PartialView(model);
            
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(CreateAuctionViewModel model)
        {
            Auction auction = new Auction();
            auction.Title = model.Title;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartingTime = model.StartingTime;
            auction.EndingTime = model.EndingTime;

            //LINQ
            var pictureIDs = model.Auctionpictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ID => int.Parse(ID)).ToList();
            auction.AuctionPictures = new List<AuctionPicture>();
            auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());

            /* the above line means:
            foreach (var picIDs in pictureIDs)
            {
                AuctionPicture auctionPicture = new AuctionPicture();
                auctionPicture.PictureID = picIDs;

                auction.AuctionPictures.Add(auctionPicture);
            }*/

            service.SaveAuction(auction);
            return RedirectToAction("Listing");
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
            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
                service.DeleteAuction(auction);
                return RedirectToAction("Listing");
        }

        public ActionResult Details(int ID)
        {
             AuctionListingViewModel model = new AuctionListingViewModel();
            // AuctionViewModel model = new AuctionViewModel();
           // Auction model = new Auction();
            model.PageTitle = "Auction-item";
            model.PageDescription = "Auction item detail page";

            var auction = service.GetAuctionByID(ID);
            return View(auction);
        }

    }

}