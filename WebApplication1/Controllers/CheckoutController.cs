using HotpotLibrary.DTO;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService _checkoutService;

        public CheckoutController(CheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("add-to-cart")]
        public IActionResult AddToCart([FromBody] CartDTO cartDTO)
        {
            _checkoutService.AddItemToCart(cartDTO);
            return Ok();
        }

        [HttpPost("place-order")]
        public IActionResult PlaceOrder([FromBody] OrderDTO orderDTO)
        {
            _checkoutService.PlaceOrder(orderDTO);
            return Ok();
        }

        [HttpPost("process-payment")]
        public IActionResult ProcessPayment([FromBody] PaymentDTO paymentDTO)
        {
            _checkoutService.ProcessPayment(paymentDTO);
            return Ok();
        }
    }
}
