using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateOrderStatus(int id, [FromBody] string status)
        {
            _orderService.UpdateOrderStatus(id, status);
            return NoContent();
        }


        [HttpGet("{id}")]
        public ActionResult<OrderDTO> GetOrder(int id)
        {
            var orderDto = _orderService.GetOrderById(id);
            if (orderDto == null)
            {
                return NotFound();
            }
            return Ok(orderDto);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<OrderDTO>> GetOrdersByUserId(int userId)
        {
            var orders = _orderService.GetOrdersByUserId(userId);
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public ActionResult<List<OrderDTO>> GetOrdersByRestaurantId(int restaurantId)
        {
            var orders = _orderService.GetOrdersByRestaurantId(restaurantId);
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        //[HttpPost]
        //public IActionResult AddOrder([FromBody] Order order)
        //{
        //    _orderService.AddOrder(order);
        //    return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
        //}

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDTO orderDto)
        {
                _orderService.CreateOrder(orderDto);
                return Ok(orderDto);
            
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDTO orderDto)
        {
            if (id != orderDto.OrderID)
            {
                return BadRequest();
            }
            _orderService.UpdateOrder(id,orderDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
