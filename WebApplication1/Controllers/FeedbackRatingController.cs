using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackRatingsController : ControllerBase
    {
        private readonly FeedbackRatingsService _feedbackRatingsService;

        public FeedbackRatingsController(FeedbackRatingsService feedbackRatingsService)
        {
            _feedbackRatingsService = feedbackRatingsService;
        }

        [HttpGet("{id}")]
        public ActionResult<FeedbackRating> GetFeedbackRating(int id)
        {
            var feedbackRating = _feedbackRatingsService.GetFeedbackRatingById(id);
            if (feedbackRating == null)
            {
                return NotFound();
            }
            return Ok(feedbackRating);
        }
        [HttpGet("restaurant/{restaurantId}")]

        public ActionResult<List<FeedbackRatingDTO>> GetFeedbackRatingsByRestaurantId(int restaurantId)
        {
            var feedbackRatings = _feedbackRatingsService.GetFeedbackRatingsByRestaurantId(restaurantId);
            if (feedbackRatings == null || !feedbackRatings.Any())
            {
                return NotFound(); // Return 404 if no feedback ratings are found
            }
            return Ok(feedbackRatings); // Return 200 with the list of feedback ratings
        }


        [HttpGet]
        public ActionResult<IEnumerable<FeedbackRating>> GetAllFeedbackRatings()
        {
            return Ok(_feedbackRatingsService.GetAllFeedbackRatings());
        }

        [HttpPost]
        public IActionResult AddFeedbackRating([FromBody] FeedbackRatingDTO feedbackRatingDto)
        {
            _feedbackRatingsService.CreateFeedbackRating(feedbackRatingDto);
            return CreatedAtAction(nameof(GetFeedbackRating), new { id = feedbackRatingDto.FeedbackRatingID }, feedbackRatingDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFeedbackRating(int id, [FromBody] FeedbackRatingDTO feedbackRatingDto)
        {
            if (id != feedbackRatingDto.FeedbackRatingID)
            {
                return BadRequest();
            }
            _feedbackRatingsService.UpdateFeedbackRating(id,feedbackRatingDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeedbackRating(int id)
        {
            _feedbackRatingsService.DeleteFeedbackRating(id);
            return NoContent();
        }
    }
}
