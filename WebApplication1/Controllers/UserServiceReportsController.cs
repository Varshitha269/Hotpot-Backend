using Asp.Versioning;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user")]
    public class UserServiceReportsController : ControllerBase
    {
        private readonly IUserServiceReportsInterface _userService;

        public UserServiceReportsController(IUserServiceReportsInterface userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO userDto)
        {
            _userService.RegisterUser(userDto);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var user = _userService.Login(loginDto.UsernameOrEmail, loginDto.Password);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _userService.Logout();
            return Ok();
        }

        [HttpGet("user/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("menus")]
        public IActionResult GetAllMenus()
        {
            var menus = _userService.GetAllMenus();
            return Ok(menus);
        }

        [HttpGet("search")]
        public IActionResult SearchMenuItems([FromQuery] string query)
        {
            var items = _userService.SearchMenuItems(query);
            return Ok(items);
        }

        [HttpGet("menu/{id}")]
        public IActionResult GetMenuItemById(int id)
        {
            var item = _userService.GetMenuItemById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }



        [HttpGet("cart/{userId}")]
        public IActionResult GetCartContents(int userId)
        {
            var cart = _userService.GetCartContents(userId);
            return Ok(cart);
        }

        [HttpGet("order/{orderId}")]
        public IActionResult GetOrderDetails(int orderId)
        {
            var order = _userService.GetOrderDetails(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("order/history/{userId}")]
        public IActionResult GetOrderHistory(int userId)
        {
            var orders = _userService.GetOrderHistory(userId);
            return Ok(orders);
        }
    }
}
