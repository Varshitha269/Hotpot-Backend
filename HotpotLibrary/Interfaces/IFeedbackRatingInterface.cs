using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IFeedbackRatingInterface
    {
        FeedbackRatingDTO GetFeedbackRatingById(int id);
        public List<FeedbackRatingDTO> GetFeedbackRatingsByRestaurantId(int restaurantId);
        IEnumerable<FeedbackRatingDTO> GetAllFeedbackRatings();
        void CreateFeedbackRating(FeedbackRatingDTO feedbackRatingDto);
        void UpdateFeedbackRating(int id, FeedbackRatingDTO feedbackRatingDto);
        void DeleteFeedbackRating(int id);
    }
}
