using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsService _orderDetailsService;

        public OrderDetailsController(OrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDetail> GetOrderDetails(int id)
        {
            var orderDetails = _orderDetailsService.GetOrderDetailsById(id);
            if (orderDetails == null)
            {
                return NotFound();
            }
            return Ok(orderDetails);
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDetail>> GetAllOrderDetails()
        {
            return Ok(_orderDetailsService.GetAllOrderDetails());
        }

        [HttpPost]
        public IActionResult AddOrderDetails([FromBody] OrderDetailDTO orderDetails)
        {
            _orderDetailsService.CreateOrderDetails(orderDetails);
            return CreatedAtAction(nameof(GetOrderDetails), new { id = orderDetails.OrderID }, orderDetails);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetails(int id, [FromBody] OrderDetailDTO orderDetails)
        {
            if (id != orderDetails.OrderID)
            {
                return BadRequest();
            }
            _orderDetailsService.UpdateOrderDetails(id,orderDetails);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetails(int id)
        {
            _orderDetailsService.DeleteOrderDetails(id);
            return NoContent();
        }
    }
}
