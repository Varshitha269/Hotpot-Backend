using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id}")]
        public ActionResult<CartDTO> GetCart(int id)
        {
            var cartDto = _cartService.GetCartById(id);
            if (cartDto == null)
            {
                return NotFound();
            }
            return Ok(cartDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CartDTO>> GetAllCarts()
        {
            return Ok(_cartService.GetAllCarts());
        }

        [HttpPost]
        public IActionResult AddCart([FromBody] CartDTO cartDto)
        {
            _cartService.CreateCart(cartDto);
            return CreatedAtAction(nameof(GetCart), new { id = cartDto.CartID }, cartDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCart(int id, [FromBody] CartDTO cartDto)
        {
            if (id != cartDto.CartID)
            {
                return BadRequest();
            }
            _cartService.UpdateCart(id,cartDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            _cartService.DeleteCart(id);
            return NoContent();
        }
    }
}
