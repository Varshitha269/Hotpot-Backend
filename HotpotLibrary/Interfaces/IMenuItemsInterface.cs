using System.Collections.Generic;
using HotpotLibrary.DTO;
using HotpotLibrary.Models;

public interface IMenuItemRepository
{
    IEnumerable<MenuItemDTO> GetAllMenuItems();
    public IEnumerable<MenuItemDTO> GetAllDistinctMenuItems();
    MenuItemDTO GetMenuItemById(int menuItemId);
    public List<MenuItemDTO> GetMenuItemsByMenuId(int menuId);
    void CreateMenuItem(MenuItemDTO menuItemDto);
    void UpdateMenuItem(int id, MenuItemDTO menuItemDto);
    void DeleteMenuItem(int menuItemId);
    IEnumerable<MenuItemDTO> GetMenuItemsByCategory(string category);
    IEnumerable<MenuItemDTO> GetMenuItemsByLeastPrice();
    IEnumerable<MenuItemDTO> GetMenuItemsByTasteInfo(string tasteInfo);
    IEnumerable<MenuItemDTO> GetMenuItemsBySpecialDietaryInfo(string dietaryInfo);
    IEnumerable<MenuItemDTO> GetMenuItemsByNutritionalInfo(string nutritionalInfo);
    IEnumerable<MenuItemDTO> GetMenuItemsByHighDiscount();
}

