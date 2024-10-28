using HotpotLibrary.Models;
using HotpotLibrary.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotpotAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("veg-items/{restaurantId}")]
        public ActionResult<List<MenuItem>> GetVegItems(int restaurantId)
        {
            var vegItems = _reportService.GetVegItemsByRestaurant(restaurantId);
            return Ok(vegItems);
        }

        [HttpGet("non-veg-items/{restaurantId}")]
        public ActionResult<List<MenuItem>> GetNonVegItems(int restaurantId)
        {
            var nonVegItems = _reportService.GetNonVegItemsByRestaurant(restaurantId);
            return Ok(nonVegItems);
        }

        [HttpGet("total-menu-items/{restaurantId}")]
        public ActionResult<int> GetTotalMenuItems(int restaurantId)
        {
            var totalItems = _reportService.GetTotalMenuItemsByRestaurant(restaurantId);
            return Ok(totalItems);
        }

        [HttpGet("total-categories/{restaurantId}")]
        public ActionResult<int> GetTotalCategories(int restaurantId)
        {
            var categories = _reportService.GetTotalCategoriesByRestaurant(restaurantId);
            return Ok(categories);
        }

        [HttpGet("total-orders/{restaurantId}")]
        public ActionResult<int> GetTotalOrders(int restaurantId)
        {
            var totalOrders = _reportService.GetTotalOrdersByRestaurant(restaurantId);
            return Ok(totalOrders);
        }

        [HttpGet("total-revenue/{restaurantId}")]
        public ActionResult<decimal> GetTotalRevenue(int restaurantId)
        {
            var totalRevenue = _reportService.GetTotalRevenueByRestaurant(restaurantId);
            return Ok(totalRevenue);
        }

        [HttpGet("revenue/{restaurantId}/{period}")]
        public ActionResult<decimal> GetRevenueByPeriod(int restaurantId, string period)
        {
            var revenue = _reportService.GetRevenueByRestaurant(restaurantId, period);
            return Ok(revenue);
        }

        [HttpGet("average-order-value/{restaurantId}")]
        public ActionResult<decimal> GetAverageOrderValue(int restaurantId)
        {
            var avgOrderValue = _reportService.GetAverageOrderValueByRestaurant(restaurantId);
            return Ok(avgOrderValue);
        }

        [HttpGet("top-selling-items/{restaurantId}/{topN}")]
        public ActionResult<List<MenuItem>> GetTopSellingItems(int restaurantId, int topN)
        {
            var topSellingItems = _reportService.GetTopSellingItemsByRestaurant(restaurantId, topN);
            return Ok(topSellingItems);
        }

        [HttpGet("least-selling-items/{restaurantId}/{bottomN}")]
        public ActionResult<List<MenuItem>> GetLeastSellingItems(int restaurantId, int bottomN)
        {
            var leastSellingItems = _reportService.GetLeastSellingItemsByRestaurant(restaurantId, bottomN);
            return Ok(leastSellingItems);
        }

        [HttpGet("regular-users/{restaurantId}/{minOrders}")]
        public ActionResult<List<User>> GetRegularUsers(int restaurantId, int minOrders)
        {
            var regularUsers = _reportService.GetRegularUsersByRestaurant(restaurantId, minOrders);
            return Ok(regularUsers);
        }
    }
}
