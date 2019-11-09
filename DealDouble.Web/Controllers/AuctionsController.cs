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
        CategoriesService categoriesService = new CategoriesService();
        // GET: Auctions
        public ActionResult Index(int? categoryID, string SearchTerm, int? pageNo)
        {
            
            AuctionListingViewModel model = new AuctionListingViewModel();
            model.PageTitle = "Auctions";
            model.PageDescription = "Auction listing page";

            model.CategoryID = categoryID;
            model.SearchTerm = SearchTerm;
            model.PageNo = pageNo;

            return View(model);
            
        }

        public ActionResult Listing( int? categoryID, string searchTerm, int? pageNo)
        {
            var pageSize = 5 ;
            AuctionListingViewModel model = new AuctionListingViewModel();
            model.Auctions = auctionsService.SearchAuctions(categoryID , searchTerm, pageNo, pageSize);
            var totalAuctions = auctionsService.GetAuctionCount();

            model.Pager = new Pager(totalAuctions, pageNo, pageSize);
            
            return PartialView(model);
            
        }
        public ActionResult Create()
        {
            CreateAuctionViewModel model = new CreateAuctionViewModel();
            model.Categories = categoriesService.GetAllCategories();
            return PartialView(model);
        }
        [HttpPost]
        public JsonResult Create(CreateAuctionViewModel model)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {

                Auction auction = new Auction();
                auction.Title = model.Title;
                auction.CategoryID = model.CategoryID;
                auction.Description = model.Description;
                auction.ActualAmount = model.ActualAmount;
                auction.StartingTime = model.StartingTime;
                auction.EndingTime = model.EndingTime;

                if (!string.IsNullOrEmpty(model.Auctionpictures))
                {
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
                }
                auctionsService.SaveAuction(auction);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false, Error= "Unable to save Auction, please enter valid values!" };
            }
            return result;
        }

        public ActionResult Edit(int ID)
        {
            CreateAuctionViewModel model = new CreateAuctionViewModel();
            var auction = auctionsService.GetAuctionByID(ID);
            model.ID = auction.ID;
            model.Title = auction.Title;
            model.CategoryID = auction.CategoryID;
            model.Description = auction.Description;
            model.ActualAmount = auction.ActualAmount;
            model.StartingTime = auction.StartingTime;
            model.EndingTime = auction.EndingTime;

            model.Categories = categoriesService.GetAllCategories();
            model.AuctionPicturesList = auction.AuctionPictures;

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(CreateAuctionViewModel model)
        {
            Auction auction = new Auction();
            auction.ID = model.ID;
            auction.Title = model.Title;
            auction.CategoryID = model.CategoryID;
            auction.Description = model.Description;
            auction.ActualAmount = model.ActualAmount;
            auction.StartingTime = model.StartingTime;
            auction.EndingTime = model.EndingTime;

            if (!string.IsNullOrEmpty(model.Auctionpictures))
            {
                //LINQ
                var pictureIDs = model.Auctionpictures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ID => int.Parse(ID)).ToList();
                auction.AuctionPictures = new List<AuctionPicture>();
                auction.AuctionPictures.AddRange(pictureIDs.Select(x => new AuctionPicture() { PictureID = x }).ToList());
            }

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