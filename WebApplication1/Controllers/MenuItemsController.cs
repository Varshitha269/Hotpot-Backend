using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HotpotLibrary.Models;
using Microsoft.Extensions.Logging;
using HotpotLibrary.DTO;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly MenuItemService _menuItemService;
    private readonly ILogger<MenuItemController> _logger;

    public MenuItemController(MenuItemService menuItemService, ILogger<MenuItemController> logger)
    {
        _menuItemService = menuItemService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MenuItemDTO>> GetAllMenuItems()
    {
        _logger.LogInformation("GET all menu items called");
        var menuItems = _menuItemService.GetAllMenuItems();
        return Ok(menuItems);
    }


    [HttpGet("distinct-menu-items")]
    public IActionResult GetAllDistinctMenuItems()
    {
        _logger.LogInformation("Controller: Fetching all distinct menu items.");

        try
        {
            var distinctMenuItems = _menuItemService.GetAllDistinctMenuItems();

            if (distinctMenuItems == null || !distinctMenuItems.Any())
            {
                _logger.LogWarning("No distinct menu items found.");
                return NotFound("No distinct menu items found.");
            }

            return Ok(distinctMenuItems); // Return the list of distinct menu items with a 200 OK status
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching distinct menu items.");
            return StatusCode(500, "An error occurred while fetching distinct menu items.");
        }
    }



    [HttpGet("{id}")]
    public ActionResult<MenuItemDTO> GetMenuItemById(int id)
    {
        _logger.LogInformation($"GET menu item by ID {id} called");
        var menuItem = _menuItemService.GetMenuItemById(id);
        if (menuItem == null) return NotFound();
        return Ok(menuItem);
    }
    [HttpGet("menu/{menuId}")]
    public ActionResult<List<MenuItemDTO>> GetMenuItemsByMenuId(int menuId)
    {
        _logger.LogInformation($"GET menu items by Menu ID {menuId} called");
        var menuItems = _menuItemService.GetMenuItemsByMenuId(menuId);

        if (menuItems == null || !menuItems.Any())
            return NotFound(); 

        return Ok(menuItems); 
    }

    [HttpPost]
    public ActionResult CreateMenuItem(MenuItemDTO menuItemDto)
    {
        _logger.LogInformation("POST create menu item called");
        _menuItemService.CreateMenuItem(menuItemDto);
        return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItemDto.MenuItemID }, menuItemDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateMenuItem(int id, MenuItemDTO menuItemDto)
    {
        _logger.LogInformation($"PUT update menu item ID {id} called");
        if (id != menuItemDto.MenuItemID) return BadRequest();
        _menuItemService.UpdateMenuItem(id,menuItemDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteMenuItem(int id)
    {
        _logger.LogInformation($"DELETE menu item ID {id} called");
        _menuItemService.DeleteMenuItem(id);
        return NoContent();
    }

    [HttpGet("category/{category}")]
    public ActionResult<IEnumerable<MenuItemDTO>> GetMenuItemsByCategory(string category)
    {
        _logger.LogInformation($"Controller: Fetching menu items by category {category}.");
        var items = _menuItemService.GetMenuItemsByCategory(category);
        return Ok(items);
    }

    [HttpGet("least-price")]
    public ActionResult<IEnumerable<MenuItemDTO>> GetMenuItemsByLeastPrice()
    {
        _logger.LogInformation("Controller: Fetching menu items by least price.");
        var items = _menuItemService.GetMenuItemsByLeastPrice();
        return Ok(items);
    }

    [HttpGet("taste-info/{tasteInfo}")]
    public ActionResult<IEnumerable<MenuItemDTO>> GetMenuItemsByTasteInfo(string tasteInfo)
    {
        _logger.LogInformation($"Controller: Fetching menu items by taste info {tasteInfo}.");
        var items = _menuItemService.GetMenuItemsByTasteInfo(tasteInfo);
        return Ok(items);
    }

    [HttpGet("special-dietary-info/{specialDietaryInfo}")]
    public ActionResult<IEnumerable<MenuItemDTO>> GetMenuItemsBySpecialDietaryInfo(string specialDietaryInfo)
    {
        _logger.LogInformation($"Controller: Fetching menu items by special dietary info {specialDietaryInfo}.");
        var items = _menuItemService.GetMenuItemsBySpecialDietaryInfo(specialDietaryInfo);
        return Ok(items);
    }

    [HttpGet("nutritional-info/{nutritionalInfo}")]
    public ActionResult<IEnumerable<MenuItemDTO>> GetMenuItemsByNutritionalInfo(string nutritionalInfo)
    {
        _logger.LogInformation($"Controller: Fetching menu items by nutritional info {nutritionalInfo}.");
        var items = _menuItemService.GetMenuItemsByNutritionalInfo(nutritionalInfo);
        return Ok(items);
    }

    [HttpGet("high-discount")]
    public ActionResult<IEnumerable<MenuItemDTO>> GetMenuItemsByHighDiscount()
    {
        _logger.LogInformation("Controller: Fetching menu items with high discount.");
        var items = _menuItemService.GetMenuItemsByHighDiscount();
        return Ok(items);
    }
}


