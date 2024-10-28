
using System.Collections.Generic;
using HotpotLibrary.DTO;
using HotpotLibrary.Models;
public interface IMenuRepository
{
    int? GetRestaurantIdByMenuId(int menuId);
    IEnumerable<MenuDTO> GetAllMenus();
    MenuDTO GetMenuById(int menuId);
    public List<MenuDTO> GetMenusByRestaurantId(int  restaurantId);
    void CreateMenu(MenuDTO menuDto);
    void UpdateMenu(int id, MenuDTO menuDto);
    void DeleteMenu(int menuId);

    
}
