using DealDouble.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealDouble.Web.ViewModels
{
    public class AuctionListingViewModel : PageViewModel
    {
        public List<Auction> Auctions { get; set; }
        public int? CategoryID { get; set; }
        public string SearchTerm { get; set; }
        public int? PageNo { get; set; }
        public Pager Pager { get; set; }
    }

    public class AuctionViewModel : PageViewModel
    {
        public List<Auction> AllAuctions { get; set; }
        public List<Auction> PromotedAuctions { get; set; }
        
    }
    public class AuctionDetailsViewModel : PageViewModel
    {
        public Auction Auction { get; set; }
    }
    public class CreateAuctionViewModel : PageViewModel
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Minimun length should be 5 characters")]
        [MaxLength(150, ErrorMessage = "Maximun length should be 5 characters")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(100, 10000, ErrorMessage = "Actual amount range should from 100 to 10000")]
        public decimal ActualAmount { get; set; }
        public DateTime? StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }
        public string Auctionpictures { get; set; }
        public List<Category> Categories { get; set; }
        public List<AuctionPicture> AuctionPicturesList { get; set; }
    }
}