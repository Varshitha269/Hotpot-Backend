using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.DTO
{
    public class FeedbackRatingDTO
    {
        public int FeedbackRatingID { get; set; }
        public int UserID { get; set; }
        public int RestaurantID { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
