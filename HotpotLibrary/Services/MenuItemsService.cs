using System.Collections.Generic;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using log4net;

public class MenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private static readonly ILog _logger = LogManager.GetLogger(typeof(MenuItemService));

    public MenuItemService(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public IEnumerable<MenuItemDTO> GetAllDistinctMenuItems()
    {
        _logger.Info("Fetching all distinct menu items from the service layer.");

        // Call the method to get distinct menu items instead of all menu items
        return _menuItemRepository.GetAllDistinctMenuItems();
    }


    public IEnumerable<MenuItemDTO> GetAllMenuItems()
    {
        _logger.Info("Fetching all menu items from the service layer.");
        return _menuItemRepository.GetAllMenuItems();
    }

    public MenuItemDTO GetMenuItemById(int menuItemId)
    {
        _logger.Info($"Fetching menu item by ID {menuItemId} from the service layer.");
        return _menuItemRepository.GetMenuItemById(menuItemId);
    }

    public List<MenuItemDTO> GetMenuItemsByMenuId(int menuId)
    {
        _logger.Info($"Fetching menu items by Menu ID {menuId} from the service layer.");
        return _menuItemRepository.GetMenuItemsByMenuId(menuId);
    }

    public void CreateMenuItem(MenuItemDTO menuItemDto)
    {
        if (menuItemDto == null)
        {
            throw new ArgumentNullException(nameof(menuItemDto), "MenuItemDTO cannot be null.");
        }

        _logger.Info("Creating a new menu item in the service layer.");
        _menuItemRepository.CreateMenuItem(menuItemDto);
        
    }

    public void UpdateMenuItem(int id, MenuItemDTO menuItemDto)
    {
        if (menuItemDto == null)
        {
            throw new ArgumentNullException(nameof(menuItemDto), "MenuItemDTO cannot be null.");
        }

        _logger.Info($"Updating menu item ID {id} in the service layer.");
        _menuItemRepository.UpdateMenuItem(id, menuItemDto);
        
    }

    public void DeleteMenuItem(int menuItemId)
    {
        _logger.Info($"Deleting menu item ID {menuItemId} in the service layer.");
        _menuItemRepository.DeleteMenuItem(menuItemId);
        
    }

    public IEnumerable<MenuItemDTO> GetMenuItemsByCategory(string category)
    {
        _logger.Info($"Fetching menu items by category: {category}.");
        return _menuItemRepository.GetMenuItemsByCategory(category);
    }

    public IEnumerable<MenuItemDTO> GetMenuItemsByLeastPrice()
    {
        _logger.Info("Fetching menu items by least price.");
        return _menuItemRepository.GetMenuItemsByLeastPrice();
    }

    public IEnumerable<MenuItemDTO> GetMenuItemsByTasteInfo(string tasteInfo)
    {
        _logger.Info($"Fetching menu items by taste info: {tasteInfo}.");
        return _menuItemRepository.GetMenuItemsByTasteInfo(tasteInfo);
    }

    public IEnumerable<MenuItemDTO> GetMenuItemsBySpecialDietaryInfo(string dietaryInfo)
    {
        _logger.Info($"Fetching menu items by special dietary info: {dietaryInfo}.");
        return _menuItemRepository.GetMenuItemsBySpecialDietaryInfo(dietaryInfo);
    }

    public IEnumerable<MenuItemDTO> GetMenuItemsByNutritionalInfo(string nutritionalInfo)
    {
        _logger.Info($"Fetching menu items by nutritional info: {nutritionalInfo}.");
        return _menuItemRepository.GetMenuItemsByNutritionalInfo(nutritionalInfo);
    }

    public IEnumerable<MenuItemDTO> GetMenuItemsByHighDiscount()
    {
        _logger.Info("Fetching menu items by high discount.");
        return _menuItemRepository.GetMenuItemsByHighDiscount();
    }
}
