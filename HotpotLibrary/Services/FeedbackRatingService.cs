using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using log4net;
using System;
using System.Collections.Generic;

namespace HotpotLibrary.Services
{
    public class FeedbackRatingsService
    {
        private readonly IFeedbackRatingInterface _feedbackRatingsRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(FeedbackRatingsService));

        public FeedbackRatingsService(IFeedbackRatingInterface feedbackRatingsRepository)
        {
            _feedbackRatingsRepository = feedbackRatingsRepository;
        }

        public FeedbackRatingDTO GetFeedbackRatingById(int id)
        {
            try
            {
                _logger.Info($"Fetching feedback rating with ID {id}.");
                var feedbackRating = _feedbackRatingsRepository.GetFeedbackRatingById(id);
                _logger.Info($"Successfully fetched feedback rating with ID {id}.");
                return feedbackRating;
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while fetching feedback rating with ID {id}.", ex);
                throw; // Re-throw the exception after logging
            }
        }

        public List<FeedbackRatingDTO> GetFeedbackRatingsByRestaurantId(int restaurantId)
        {
            try
            {
                _logger.Info($"Fetching feedback ratings for RestaurantID {restaurantId}.");
                var feedbackRatings = _feedbackRatingsRepository.GetFeedbackRatingsByRestaurantId(restaurantId);
                if (feedbackRatings == null || !feedbackRatings.Any())
                {
                    _logger.Info($"No feedback ratings found for RestaurantID {restaurantId}.");
                    return null;
                }
                _logger.Info($"Successfully fetched feedback ratings for RestaurantID {restaurantId}.");
                return feedbackRatings;
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while fetching feedback ratings for RestaurantID {restaurantId}.", ex);
                throw; // Re-throw the exception after logging
            }
        }


        public IEnumerable<FeedbackRatingDTO> GetAllFeedbackRatings()
        {
            try
            {
                _logger.Info("Fetching all feedback ratings.");
                var feedbackRatings = _feedbackRatingsRepository.GetAllFeedbackRatings();
                _logger.Info("Successfully fetched all feedback ratings.");
                return feedbackRatings;
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred while fetching all feedback ratings.", ex);
                throw;
            }
        }

        public void CreateFeedbackRating(FeedbackRatingDTO feedbackRatingDto)
        {
            if (feedbackRatingDto == null)
            {
                throw new ArgumentNullException(nameof(feedbackRatingDto), "FeedbackRatingDTO cannot be null.");
            }

            try
            {
                _logger.Info($"Creating feedback rating with ID {feedbackRatingDto.FeedbackRatingID}.");
                _feedbackRatingsRepository.CreateFeedbackRating(feedbackRatingDto);
                _logger.Info($"Successfully created feedback rating with ID {feedbackRatingDto.FeedbackRatingID}.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while creating feedback rating with ID {feedbackRatingDto.FeedbackRatingID}.", ex);
                throw;
            }
        }

        public void UpdateFeedbackRating(int id, FeedbackRatingDTO feedbackRatingDto)
        {
            if (feedbackRatingDto == null)
            {
                throw new ArgumentNullException(nameof(feedbackRatingDto), "FeedbackRatingDTO cannot be null.");
            }

            try
            {
                _logger.Info($"Updating feedback rating with ID {id}.");
                _feedbackRatingsRepository.UpdateFeedbackRating(id, feedbackRatingDto);
                _logger.Info($"Successfully updated feedback rating with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while updating feedback rating with ID {id}.", ex);
                throw;
            }
        }

        public void DeleteFeedbackRating(int id)
        {
            try
            {
                _logger.Info($"Deleting feedback rating with ID {id}.");
                _feedbackRatingsRepository.DeleteFeedbackRating(id);
                _logger.Info($"Successfully deleted feedback rating with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while deleting feedback rating with ID {id}.", ex);
                throw;
            }
        }
    }
}
