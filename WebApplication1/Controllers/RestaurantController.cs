using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HotpotLibrary.Models;
using Microsoft.Extensions.Logging;
using HotpotLibrary.DTO;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantService _restaurantService;
    private readonly ILogger<RestaurantController> _logger;

    public RestaurantController(RestaurantService restaurantService, ILogger<RestaurantController> logger)
    {
        _restaurantService = restaurantService;
        _logger = logger;
    }

    [HttpGet("GetRestaurantIdByName/{restaurantName}")]
    public IActionResult GetRestaurantIdByName(string restaurantName)
    {
        _logger.LogInformation($"Controller: Fetching restaurant ID for Name {restaurantName}.");

        var restaurantId = _restaurantService.GetRestaurantIdByName(restaurantName);

        if (restaurantId == null)
        {
            _logger.LogInformation($"Controller: Restaurant with Name {restaurantName} not found.");
            return NotFound($"Restaurant with Name {restaurantName} not found.");
        }

        return Ok(restaurantId);
    }


    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDTO>> GetAllRestaurants()
    {
        _logger.LogInformation("Controller: Fetching all restaurants.");
        var restaurants = _restaurantService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public ActionResult<RestaurantDTO> GetRestaurantById(int id)
    {
        _logger.LogInformation($"Controller: Fetching restaurant with ID {id}.");
        var restaurant = _restaurantService.GetRestaurantById(id);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpPost]
    public IActionResult CreateRestaurant(RestaurantDTO restaurantDto)
    {
        _logger.LogInformation($"Controller: Creating restaurant with Name {restaurantDto.Name}.");
        _restaurantService.CreateRestaurant(restaurantDto);
        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurantDto.RestaurantID }, restaurantDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRestaurant(int id, RestaurantDTO restaurantDto)
    {
        if (id != restaurantDto.RestaurantID)
        {
            return BadRequest();
        }

        _logger.LogInformation($"Controller: Updating restaurant with ID {id}.");
        _restaurantService.UpdateRestaurant(id,restaurantDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRestaurant(int id)
    {
        _logger.LogInformation($"Controller: Deleting restaurant with ID {id}.");
        _restaurantService.DeleteRestaurant(id);
        return NoContent();
    }
    [HttpGet("search")]
    public ActionResult<RestaurantDTO> GetRestaurantByNameAndLocation([FromQuery] string name, [FromQuery] string location)
    {
        _logger.LogInformation($"Controller: Fetching restaurant with Name {name} and Location {location}.");
        var restaurant = _restaurantService.GetRestaurantByNameAndLocation(name, location);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    // Get restaurant by email
    [HttpGet("email")]
    public ActionResult<RestaurantDTO> GetRestaurantByEmail([FromQuery] string email)
    {
        _logger.LogInformation($"Controller: Fetching restaurant with Email {email}.");
        var restaurant = _restaurantService.GetRestaurantByEmail(email);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    // Get all restaurants by city
    [HttpGet("city/{city}")]
    public ActionResult<IEnumerable<RestaurantDTO>> GetAllRestaurantsByCity(string city)
    {
        _logger.LogInformation($"Controller: Fetching all restaurants in City {city}.");
        var restaurants = _restaurantService.GetAllRestaurantsByCity(city);
        return Ok(restaurants);
    }
}
