using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Ensure you have this namespace for ILogger
using System.Collections.Generic;
using HotpotLibrary.DTO; // Adjust the namespace based on your project structure
using HotpotLibrary.Services; // Ensure you include the namespace for your service

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantMenuItemController : ControllerBase
    {
        private readonly RestaurantMenuItemService _restaurantMenuItemService;
        private readonly ILogger<RestaurantMenuItemController> _logger;

        public RestaurantMenuItemController(RestaurantMenuItemService restaurantMenuItemService, ILogger<RestaurantMenuItemController> logger)
        {
            _restaurantMenuItemService = restaurantMenuItemService;
            _logger = logger;
        }

        // GET api/restaurantmenuitem/{menuItemName}
        [HttpGet("{menuItemName}")]
        public ActionResult<IEnumerable<RestaurantMenuItem>> GetRestaurantsWithMenuItem(string menuItemName)
        {
            _logger.LogInformation($"GET restaurants with menu item called for: {menuItemName}");

            var restaurants = _restaurantMenuItemService.GetRestaurantsWithMenuItem(menuItemName);

            if (restaurants == null || !restaurants.Any())
            {
                _logger.LogInformation("No restaurants found for the specified menu item.");
                return NotFound();
            }

            return Ok(restaurants);
        }
    }
}
