using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HotpotLibrary.Models;
using Microsoft.Extensions.Logging;
using HotpotLibrary.DTO;

[ApiController]
[Route("api/[controller]")]

public class MenuController : ControllerBase
{
    private readonly MenuService _menuService;
    private readonly ILogger<MenuController> _logger;

    public MenuController(MenuService menuService, ILogger<MenuController> logger)
    {
        _menuService = menuService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MenuDTO>> GetAllMenus()
    {
        _logger.LogInformation("GET all menus called");
        var menus = _menuService.GetAllMenus();
        return Ok(menus);
    }

    [HttpGet("{id}")]
    public ActionResult<MenuDTO> GetMenuById(int id)
    {
        _logger.LogInformation($"GET menu by ID {id} called");
        var menu = _menuService.GetMenuById(id);
        if (menu == null) return NotFound();
        return Ok(menu);
    }

    [HttpGet("restaurant/{restaurantId}")]
    public ActionResult<List<MenuDTO>> GetMenusByRestaurantId(int restaurantId)
    {
        _logger.LogInformation($"GET menus by Restaurant ID {restaurantId} called");

        var menus = _menuService.GetMenusByRestaurantId(restaurantId);

        if (menus == null || !menus.Any())
        {
            _logger.LogWarning($"No menus found for Restaurant ID {restaurantId}");
            return NotFound();
        }

        _logger.LogInformation($"Successfully fetched menus for Restaurant ID {restaurantId}");
        return Ok(menus);
    }


    [HttpPost]
    public ActionResult CreateMenu(MenuDTO menuDto)
    {
        _logger.LogInformation("POST create menu called");
        _menuService.CreateMenu(menuDto);
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateMenu(int id, MenuDTO menuDto)
    {
        _logger.LogInformation($"PUT update menu ID {id} called");
        if (id != menuDto.MenuID) return BadRequest();
        _menuService.UpdateMenu(id,menuDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteMenu(int id)
    {
        _logger.LogInformation($"DELETE menu ID {id} called");
        _menuService.DeleteMenu(id);
        return NoContent();
    }

    [HttpGet("restaurantid/{menuId}")]
    public ActionResult<int?> GetRestaurantIdByMenuId(int menuId)
    {
        _logger.LogInformation($"GET Restaurant ID for Menu ID {menuId} called");
        var restaurantId = _menuService.GetRestaurantIdByMenuId(menuId);

        if (restaurantId == null)
        {
            _logger.LogWarning($"No Restaurant found for Menu ID {menuId}");
            return NotFound();
        }

        _logger.LogInformation($"Successfully fetched Restaurant ID {restaurantId} for Menu ID {menuId}");
        return Ok(restaurantId);
    }

}

