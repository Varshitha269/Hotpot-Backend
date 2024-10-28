using Asp.Versioning;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Restaurant")]
    public class RestaurantServiceReportsController : ControllerBase
    {
        private readonly IRestaurantServiceReportsInterface _restaurantService;
        private readonly ILogger<RestaurantServiceReportsController> _log;

        public RestaurantServiceReportsController(IRestaurantServiceReportsInterface restaurantService, ILogger<RestaurantServiceReportsController> log)
        {
            _restaurantService = restaurantService;
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                _restaurantService.Login(loginDto.UsernameOrEmail, loginDto.Password);
                return Ok("Logged in successfully.");
            }
            catch (Exception ex)
            {
                _log.LogError($"Error logging in: {ex.Message}");
                return BadRequest("Error logging in.");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                _restaurantService.Logout();
                return Ok("Logged out successfully.");
            }
            catch (Exception ex)
            {
                _log.LogError($"Error logging out: {ex.Message}");
                return BadRequest("Error logging out.");
            }
        }

        [HttpPost("menuitems")]
        public IActionResult CreateMenuItem([FromBody] MenuItemDTO menuItemDto)
        {
            try
            {
                _restaurantService.CreateMenuItem(menuItemDto);
                return Ok("Menu item created successfully.");
            }
            catch (Exception ex)
            {
                _log.LogError($"Error creating menu item: {ex.Message}");
                return BadRequest("Error creating menu item.");
            }
        }

        [HttpPut("menuitems")]
        public IActionResult UpdateMenuItem([FromBody] MenuItemDTO menuItemDto)
        {
            try
            {
                _restaurantService.UpdateMenuItem(menuItemDto);
                return Ok("Menu item updated successfully.");
            }
            catch (Exception ex)
            {
                _log.LogError($"Error updating menu item: {ex.Message}");
                return BadRequest("Error updating menu item.");
            }
        }

        [HttpDelete("menuitems/{menuItemId}")]
        public IActionResult DeleteMenuItem(int menuItemId)
        {
            try
            {
                _restaurantService.DeleteMenuItem(menuItemId);
                return Ok("Menu item deleted successfully.");
            }
            catch (Exception ex)
            {
                _log.LogError($"Error deleting menu item: {ex.Message}");
                return BadRequest("Error deleting menu item.");
            }
        }

        [HttpPatch("menuitems/outofstock/{menuItemId}")]
        public IActionResult MarkMenuItemOutOfStock(int menuItemId)
        {
            try
            {
                _restaurantService.MarkMenuItemOutOfStock(menuItemId);
                return Ok("Menu item marked as out of stock.");
            }
            catch (Exception ex)
            {
                _log.LogError($"Error marking menu item as out of stock: {ex.Message}");
                return BadRequest("Error marking menu item as out of stock.");
            }
        }

        [HttpGet("orders/ongoing/{restaurantId}")]
        public IActionResult GetOngoingOrders(int restaurantId)
        {
            try
            {
                var orders = _restaurantService.GetOngoingOrders(restaurantId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _log.LogError($"Error retrieving ongoing orders: {ex.Message}");
                return BadRequest("Error retrieving ongoing orders.");
            }
        }

        [HttpPut("orders/{orderId}/status")]
        public IActionResult UpdateOrderStatus(int orderId, [FromBody] string orderStatus)
        {
            try
            {
                _restaurantService.UpdateOrderStatus(orderId, orderStatus);
                return Ok("Order status updated successfully.");
            }
            catch (Exception ex)
            {
                _log.LogError($"Error updating order status: {ex.Message}");
                return BadRequest("Error updating order status.");
            }
        }

        [HttpGet("orders/history/{restaurantId}")]
        public IActionResult GetOrderHistory(int restaurantId)
        {
            try
            {
                var orders = _restaurantService.GetOrderHistory(restaurantId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _log.LogError($"Error retrieving order history: {ex.Message}");
                return BadRequest("Error retrieving order history.");
            }
        }

        [HttpGet("items/{restaurantId}")]
        public ActionResult<List<MenuItemDTO>> GetMenuItems(int restaurantId)
        {
            try
            {
                _log.LogInformation($"Request received to get menu items for restaurant ID: {restaurantId}");

                var menuItems = _restaurantService.GetMenuItems(restaurantId);

                if (menuItems == null || !menuItems.Any())
                {
                    _log.LogInformation($"No menu items found for restaurant ID: {restaurantId}");
                    return NotFound(new { Message = "No menu items found for the specified restaurant." });
                }

                _log.LogInformation("Menu items retrieved successfully.");
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                _log.LogError($"Error retrieving menu items for restaurant ID: {restaurantId}. Exception: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while retrieving menu items." });
            }
        }
    }
}
