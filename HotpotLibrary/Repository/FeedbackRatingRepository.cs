using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class FeedbackRatingRepository : IFeedbackRatingInterface
    {
        private readonly AppDbContext _context;

        public FeedbackRatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public FeedbackRatingDTO GetFeedbackRatingById(int id)
        {
            var feedbackRating = _context.FeedbackRatings.Find(id);
            if (feedbackRating == null) return null;

            return new FeedbackRatingDTO
            {
                FeedbackRatingID = feedbackRating.FeedbackRatingID,
                UserID = feedbackRating.UserID,
                RestaurantID = feedbackRating.RestaurantID,
                Message = feedbackRating.Message,
                Rating = feedbackRating.Rating,
                CreatedDate = (DateTime)feedbackRating.CreatedDate
            };
        }

        public List<FeedbackRatingDTO> GetFeedbackRatingsByRestaurantId(int restaurantId)
        {
            var feedbackRatings = _context.FeedbackRatings
                                          .Where(fr => fr.RestaurantID == restaurantId)
                                          .ToList();

            if (!feedbackRatings.Any()) return null;

            return feedbackRatings.Select(feedbackRating => new FeedbackRatingDTO
            {
                FeedbackRatingID = feedbackRating.FeedbackRatingID,
                UserID = feedbackRating.UserID,
                RestaurantID = feedbackRating.RestaurantID,
                Message = feedbackRating.Message,
                Rating = feedbackRating.Rating,
                CreatedDate = (DateTime)feedbackRating.CreatedDate
            }).ToList();
        }


        public IEnumerable<FeedbackRatingDTO> GetAllFeedbackRatings()
        {
            return _context.FeedbackRatings.Select(feedbackRating => new FeedbackRatingDTO
            {
                FeedbackRatingID = feedbackRating.FeedbackRatingID,
                UserID = feedbackRating.UserID,
                RestaurantID = feedbackRating.RestaurantID,
                Message = feedbackRating.Message,
                Rating = feedbackRating.Rating,
                CreatedDate = (DateTime)feedbackRating.CreatedDate
            }).ToList();
        }

        public void CreateFeedbackRating(FeedbackRatingDTO feedbackRatingDto)
        {
            var feedbackRating = new FeedbackRating
            {
                FeedbackRatingID = feedbackRatingDto.FeedbackRatingID,
                UserID = feedbackRatingDto.UserID,
                RestaurantID = feedbackRatingDto.RestaurantID,
                Message = feedbackRatingDto.Message,
                Rating = feedbackRatingDto.Rating,
                CreatedDate = feedbackRatingDto.CreatedDate
            };
            _context.FeedbackRatings.Add(feedbackRating);
            _context.SaveChanges();
        }

        public void UpdateFeedbackRating(int id, FeedbackRatingDTO feedbackRatingDto)
        {
            var feedbackRating = _context.FeedbackRatings.Find(id);
            if (feedbackRating != null)
            {
                feedbackRating.UserID = feedbackRatingDto.UserID;
                feedbackRating.RestaurantID = feedbackRatingDto.RestaurantID;
                feedbackRating.Message = feedbackRatingDto.Message;
                feedbackRating.Rating = feedbackRatingDto.Rating;
                feedbackRating.CreatedDate = feedbackRatingDto.CreatedDate;

                _context.FeedbackRatings.Update(feedbackRating);
                _context.SaveChanges();
            }
        }

        public void DeleteFeedbackRating(int id)
        {
            var feedbackRating = _context.FeedbackRatings.Find(id);
            if (feedbackRating != null)
            {
                _context.FeedbackRatings.Remove(feedbackRating);
                _context.SaveChanges();
            }
        }
    }
}
