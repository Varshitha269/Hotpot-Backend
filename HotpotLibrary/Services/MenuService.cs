using System.Collections.Generic;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using log4net;

public class MenuService
{
    private readonly IMenuRepository _menuRepository;
    private static readonly ILog _logger = LogManager.GetLogger(typeof(MenuService));

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public IEnumerable<MenuDTO> GetAllMenus()
    {
        _logger.Info("Fetching all menus from the service layer.");
        return _menuRepository.GetAllMenus();
    }

    public MenuDTO GetMenuById(int menuId)
    {
        _logger.Info($"Fetching menu by ID {menuId} from the service layer.");
        return _menuRepository.GetMenuById(menuId);
    }

    public List<MenuDTO> GetMenusByRestaurantId(int restaurantId)
    {
        _logger.Info($"Fetching menus by Restaurant ID {restaurantId} from the service layer.");
        var menus = _menuRepository.GetMenusByRestaurantId(restaurantId);

        if (menus == null || !menus.Any())
        {
            _logger.Warn($"No menus found for Restaurant ID {restaurantId}.");
            return null;
        }

        _logger.Info($"Successfully fetched menus for Restaurant ID {restaurantId}.");
        return menus;
    }


    public void CreateMenu(MenuDTO menuDto)
    {
        if (menuDto == null)
        {
            throw new ArgumentNullException(nameof(menuDto), "MenuDTO cannot be null.");
        }

        _logger.Info("Creating a new menu in the service layer.");
        _menuRepository.CreateMenu(menuDto);
        
    }

    public void UpdateMenu(int id, MenuDTO menuDto)
    {
        if (menuDto == null)
        {
            throw new ArgumentNullException(nameof(menuDto), "MenuDTO cannot be null.");
        }

        _logger.Info($"Updating menu ID {id} in the service layer.");
        _menuRepository.UpdateMenu(id, menuDto);
        
    }

    public void DeleteMenu(int menuId)
    {
        _logger.Info($"Deleting menu ID {menuId} in the service layer.");
        _menuRepository.DeleteMenu(menuId);
        
    }

    public int? GetRestaurantIdByMenuId(int menuId)
    {
        _logger.Info($"Fetching Restaurant ID for Menu ID {menuId} from the service layer.");
        var restaurantId = _menuRepository.GetRestaurantIdByMenuId(menuId);

        if (restaurantId == null)
        {
            _logger.Warn($"No Restaurant found for Menu ID {menuId}.");
            return null;
        }

        _logger.Info($"Successfully fetched Restaurant ID {restaurantId} for Menu ID {menuId}.");
        return restaurantId;
    }
}
