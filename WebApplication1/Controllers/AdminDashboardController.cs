using Asp.Versioning;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //[ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboard _adminDashboardService;

        public AdminDashboardController(IAdminDashboard adminDashboardService)
        {
            _adminDashboardService = adminDashboardService;
        }

        [HttpGet("veg-nonveg-count/{restaurantId}")]
        public ActionResult<Dictionary<string, int>> GetVegNonVegItemCount(int restaurantId)
        {
            try
            {
                var vegNonVegCount = _adminDashboardService.GetVegNonVegItemCount(restaurantId);
                return Ok(vegNonVegCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("orders/last7days")]
        public ActionResult<List<Order>> GetOrdersFromLast7Days()
        {
            try
            {
                var orders = _adminDashboardService.GetOrdersFromLast7Days();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("regularusers")]
        public ActionResult<List<User>> GetRegularUsers()
        {
            try
            {
                var regularUsers = _adminDashboardService.GetRegularUsers();
                return Ok(regularUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("orders/perday")]
        public ActionResult<Dictionary<DateTime, int>> GetTotalOrdersPerDay(DateTime startDate, DateTime endDate)
        {
            try
            {
                var ordersPerDay = _adminDashboardService.GetTotalOrdersPerDay(startDate, endDate);
                return Ok(ordersPerDay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("menuitemcounts")]
        public ActionResult<Dictionary<string, Dictionary<DateTime, int>>> GetMenuItemCountsByCategory(string category, DateTime startDate, DateTime endDate)
        {
            try
            {
                var menuItemCounts = _adminDashboardService.GetMenuItemCountsByCategory(category, startDate, endDate);
                return Ok(menuItemCounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("revenue")]
        public ActionResult<Dictionary<DateTime, decimal>> GetRevenueReports(string period)
        {
            try
            {
                var revenueReports = _adminDashboardService.GetRevenueReports(period);
                return Ok(revenueReports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("topsellingitems")]
        public ActionResult<List<MenuItem>> GetTopSellingItems(string period)
        {
            try
            {
                var topSellingItems = _adminDashboardService.GetTopSellingItems(period);
                return Ok(topSellingItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("users/byfrequency")]
        public ActionResult<List<User>> GetUsersByOrderFrequency(string period)
        {
            try
            {
                var usersByOrderFrequency = _adminDashboardService.GetUsersByOrderFrequency(period);
                return Ok(usersByOrderFrequency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("averageordervalue")]
        public ActionResult<Dictionary<DateTime, decimal>> GetAverageOrderValue(string period)
        {
            try
            {
                var averageOrderValue = _adminDashboardService.GetAverageOrderValue(period);
                return Ok(averageOrderValue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        
    }
}
