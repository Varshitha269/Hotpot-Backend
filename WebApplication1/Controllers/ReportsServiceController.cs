using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportsServiceController : ControllerBase
    {
        private readonly IReportsService _reportService;

        public ReportsServiceController(IReportsService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("order-statistics")]
        public async Task<ActionResult<OrderStatisticsReport>> GetOrderStatistics()
        {
            var report = await _reportService.GetOrderStatisticsAsync();
            return Ok(report);
        }

        [HttpGet("restaurant-statistics")]
        public async Task<ActionResult<RestaurantStatisticsReport>> GetRestaurantStatistics()
        {
            var report = await _reportService.GetRestaurantStatisticsAsync();
            return Ok(report);
        }

        [HttpGet("user-statistics")]
        public async Task<ActionResult<UserStatisticsReport>> GetUserStatistics()
        {
            var report = await _reportService.GetUserStatisticsAsync();
            return Ok(report);
        }

        //[HttpGet("menu-statistics")]
        //public async Task<ActionResult<MenuStatisticsReport>> GetMenuStatistics()
        //{
        //    var report = await _reportService.GetMenuStatisticsAsync();
        //    return Ok(report);
        //}

        [HttpGet("payment-statistics")]
        public async Task<ActionResult<PaymentStatisticsReport>> GetPaymentStatistics()
        {
            var report = await _reportService.GetPaymentStatisticsAsync();
            return Ok(report);
        }
    }
}
