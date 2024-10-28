using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;

        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public MenuDTO GetMenuById(int id)
        {
            var menu = _context.Menus.Find(id);
            if (menu == null) return null;

            return new MenuDTO
            {
                MenuID = menu.MenuID,
                RestaurantID = menu.RestaurantID,
                MenuName = menu.MenuName,
                Description = menu.Description,
                CreatedDate = (DateTime)menu.CreatedDate,
                IsActive = (bool)menu.IsActive
            };
        }

        public List<MenuDTO> GetMenusByRestaurantId(int restaurantId)
        {
            var menus = _context.Menus
                .Where(m => m.RestaurantID == restaurantId)
                .ToList();

            if (menus == null || !menus.Any()) return null;

            return menus.Select(menu => new MenuDTO
            {
                MenuID = menu.MenuID,
                RestaurantID = menu.RestaurantID,
                MenuName = menu.MenuName,
                Description = menu.Description,
                CreatedDate = (DateTime)menu.CreatedDate,
                IsActive = (bool)menu.IsActive
            }).ToList();
        }


        public IEnumerable<MenuDTO> GetAllMenus()
        {
            return _context.Menus.Select(menu => new MenuDTO
            {
                MenuID = menu.MenuID,
                RestaurantID = menu.RestaurantID,
                MenuName = menu.MenuName,
                Description = menu.Description,
                CreatedDate = (DateTime)menu.CreatedDate,
                IsActive = (bool)menu.IsActive
            }).ToList();
        }

        public void CreateMenu(MenuDTO menuDto)
        {
            var menu = new Menu
            {
                MenuID = menuDto.MenuID,
                RestaurantID = menuDto.RestaurantID,
                MenuName = menuDto.MenuName,
                Description = menuDto.Description,
                CreatedDate = menuDto.CreatedDate,
                IsActive = menuDto.IsActive
            };
            _context.Menus.Add(menu);
            _context.SaveChanges();
            
        }

        public void UpdateMenu(int id, MenuDTO menuDto)
        {
            var menu = _context.Menus.Find(id);
            if (menu != null)
            {
                menu.RestaurantID = menuDto.RestaurantID;
                menu.MenuName = menuDto.MenuName;
                menu.Description = menuDto.Description;
                menu.CreatedDate = menuDto.CreatedDate;
                menu.IsActive = menuDto.IsActive;

                _context.Menus.Update(menu);
                _context.SaveChanges();
                
            }
        }

        public void DeleteMenu(int id)
        {
            var menu = _context.Menus.Find(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
                _context.SaveChanges();
                
            }
        }
        public int? GetRestaurantIdByMenuId(int menuId)
        {
            var menu = _context.Menus.Find(menuId);
            if (menu == null)
            {
                return null; // Return null if the menu is not found
            }

            return menu.RestaurantID; // Return the RestaurantID
        }
    }
}
