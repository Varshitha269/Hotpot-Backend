using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HotpotLibrary.Models;
using Microsoft.Extensions.Logging;
using HotpotLibrary.DTO;
using WebApplication1;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ActionFilter))]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(UserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
    {
        _logger.LogInformation("Controller: Fetching all users.");
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<UserDTO> GetUserById(int id)
    {
        _logger.LogInformation($"Controller: Fetching user with ID {id}.");
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser(UserDTO userDto)
    {
        _logger.LogInformation($"Controller: Creating user with Username {userDto.Username}.");
        _userService.CreateUser(userDto);
        return CreatedAtAction(nameof(GetUserById), new { id = userDto.UserID }, userDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, UserDTO userDto)
    {
        if (id != userDto.UserID)
        {
            return BadRequest();
        }

        _logger.LogInformation($"Controller: Updating user with ID {id}.");
        _userService.UpdateUser(id,userDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _logger.LogInformation($"Controller: Deleting user with ID {id}.");
        _userService.DeleteUser(id);
        return NoContent();
    }
    [HttpGet("username/{username}")]
    public ActionResult<UserDTO> GetUserByUsername(string username)
    {
        var user = _userService.GetUserByUsername(username);
        if (user == null)
        {
            return NotFound($"User with Username {username} not found.");
        }
        return Ok(user);
    }

    // GET: api/User/email/{email}
    [HttpGet("email/{email}")]
    public ActionResult<UserDTO> GetUserByEmail(string email)
    {
        var user = _userService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound($"User with Email {email} not found.");
        }
        return Ok(user);
    }

    // GET: api/User/{id}/reviews
    [HttpGet("{id}/reviews")]
    public ActionResult<IEnumerable<FeedbackRatingDTO>> GetUserReviews(int id)
    {
        var reviews = _userService.GetUserReviews(id);
        if (reviews == null || !reviews.Any())
        {
            return NotFound($"No reviews found for user with ID {id}.");
        }
        return Ok(reviews);
    }

    // GET: api/User/{id}/orders
    [HttpGet("{id}/orders")]
    public ActionResult<IEnumerable<OrderDTO>> GetAllOrdersByUserId(int id)
    {
        var orders = _userService.GetAllOrdersByUserId(id);
        if (orders == null || !orders.Any())
        {
            return NotFound($"No orders found for user with ID {id}.");
        }
        return Ok(orders);
    }

}
