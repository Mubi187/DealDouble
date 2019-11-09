using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class Auction: BaseEntity
    {
        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }     //for foreign key
        [Required]
        [MinLength(5, ErrorMessage ="Minimun length should be 5 characters")]
        [MaxLength(150, ErrorMessage = "Maximun length should be 5 characters")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(100,10000, ErrorMessage ="Actual amount range should from 100 to 10000")]
        public decimal ActualAmount { get; set; }
        public DateTime? StartingTime { get; set; }
        public Nullable<DateTime> EndingTime { get; set; }

        public virtual List<AuctionPicture> AuctionPictures { get; set; }
    }
}
