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
        AuctionsService auctionsService = new AuctionsService();
        CategoryService categoriesService = new CategoryService();
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
            model.Auctions = auctionsService.GetAllAuction();
            
                return PartialView(model);
            
        }
        public ActionResult Create()
        {
            CreateAuctionViewModel model = new CreateAuctionViewModel();
            model.Categories = categoriesService.GetAllCategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(CreateAuctionViewModel model)
        {
            Auction auction = new Auction();
            auction.Title = model.Title;
            auction.CategoryID = model.CategoryID;
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

            auctionsService.SaveAuction(auction);
            return RedirectToAction("Listing");
        }

        public ActionResult Edit(int ID)
        {
            var auction = auctionsService.GetAuctionByID(ID);
            return PartialView(auction);
        }
        [HttpPost]
        public ActionResult Edit(Auction auction)
        {
            auctionsService.UpdateAuction(auction);
            return RedirectToAction("Listing");
        }

        [HttpPost]
        public ActionResult Delete(Auction auction)
        {
                auctionsService.DeleteAuction(auction);
                return RedirectToAction("Listing");
        }

        public ActionResult Details(int ID)
        {
             AuctionDetailsViewModel model = new AuctionDetailsViewModel();
           
            model.Auction = auctionsService.GetAuctionByID(ID);

            model.PageTitle = "Auction Details:"+ model.Auction.Title;
            model.PageDescription = model.Auction.Description.Substring(0,10);
            return View(model);
        }

    }

}