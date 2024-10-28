using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public MenuItemDTO GetMenuItemById(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem == null) return null;

            return new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                MenuID = menuItem.MenuID,
                ItemName = menuItem.ItemName,
                Description = menuItem.Description,
                Category = menuItem.Category,
                Price = menuItem.Price,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                TasteInfo = menuItem.TasteInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                AvailabilityTime = menuItem.AvailabilityTime,
                IsInStock = (bool)menuItem.IsInStock,
                ImageUrl = menuItem.ImageUrl,
                IsAvailable = (bool)menuItem.IsAvailable,
                Discounts = (decimal)menuItem.Discounts,
                CreatedDate = (DateTime)menuItem.CreatedDate
            };
        }


        public List<MenuItemDTO> GetMenuItemsByMenuId(int menuId)
        {
            var menuItems = _context.MenuItems
                .Where(mi => mi.MenuID == menuId)
                .ToList();

            if (menuItems == null || !menuItems.Any())
                return new List<MenuItemDTO>(); // Return an empty list if no items found.

            return menuItems.Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                MenuID = menuItem.MenuID,
                ItemName = menuItem.ItemName,
                Description = menuItem.Description,
                Category = menuItem.Category,
                Price = menuItem.Price,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                TasteInfo = menuItem.TasteInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                AvailabilityTime = menuItem.AvailabilityTime,
                IsInStock = (bool)menuItem.IsInStock,
                ImageUrl = menuItem.ImageUrl,
                IsAvailable = (bool)menuItem.IsAvailable,
                Discounts = (decimal)menuItem.Discounts,
                CreatedDate = (DateTime)menuItem.CreatedDate
            }).ToList();
        }
        public IEnumerable<MenuItemDTO> GetAllDistinctMenuItems()
        {
            var sqlQuery = @"
        SELECT DISTINCT 
            ItemName, 
            CAST(Description AS VARCHAR(MAX)) AS Description, 
            Category, 
            Price, 
            SpecialDietaryInfo, 
            TasteInfo, 
            NutritionalInfo, 
            AvailabilityTime, 
            IsInStock, 
            ImageURL, 
            Discounts
        FROM MenuItems";

            // Execute the raw SQL query
            return _context.MenuItems
                .FromSqlRaw(sqlQuery)
                .Select(menuItem => new MenuItemDTO
                {
                    ItemName = menuItem.ItemName,
                    Description = menuItem.Description,
                    Category = menuItem.Category,
                    Price = menuItem.Price,
                    SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                    TasteInfo = menuItem.TasteInfo,
                    NutritionalInfo = menuItem.NutritionalInfo,
                    AvailabilityTime = menuItem.AvailabilityTime,
                    IsInStock = menuItem.IsInStock ?? false, // Handle nullable boolean
                    ImageUrl = menuItem.ImageUrl,
                    Discounts = menuItem.Discounts >= 0 ? menuItem.Discounts : 0m // Ensure non-negative or default to 0
                })
                .ToList();
        }







        public IEnumerable<MenuItemDTO> GetAllMenuItems()
        {
            return _context.MenuItems.Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                MenuID = menuItem.MenuID,
                ItemName = menuItem.ItemName,
                Description = menuItem.Description,
                Category = menuItem.Category,
                Price = menuItem.Price,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                TasteInfo = menuItem.TasteInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                AvailabilityTime = menuItem.AvailabilityTime,
                IsInStock = (bool)menuItem.IsInStock,
                ImageUrl = menuItem.ImageUrl,
                IsAvailable = (bool)menuItem.IsAvailable,
                Discounts = (decimal)menuItem.Discounts,
                CreatedDate = (DateTime)menuItem.CreatedDate
            }).ToList();
        }

        public void CreateMenuItem(MenuItemDTO menuItemDto)
        {
            var menuItem = new MenuItem
            {
                MenuItemID = menuItemDto.MenuItemID,
                MenuID = menuItemDto.MenuID,
                ItemName = menuItemDto.ItemName,
                Description = menuItemDto.Description,
                Category = menuItemDto.Category,
                Price = menuItemDto.Price,
                SpecialDietaryInfo = menuItemDto.SpecialDietaryInfo,
                TasteInfo = menuItemDto.TasteInfo,
                NutritionalInfo = menuItemDto.NutritionalInfo,
                AvailabilityTime = menuItemDto.AvailabilityTime,
                IsInStock = menuItemDto.IsInStock,
                ImageUrl = menuItemDto.ImageUrl,
                IsAvailable = menuItemDto.IsAvailable,
                Discounts = menuItemDto.Discounts,
                CreatedDate = menuItemDto.CreatedDate
            };
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
        }

        public void UpdateMenuItem(int id, MenuItemDTO menuItemDto)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem != null)
            {
                menuItem.MenuID = menuItemDto.MenuID;
                menuItem.ItemName = menuItemDto.ItemName;
                menuItem.Description = menuItemDto.Description;
                menuItem.Category = menuItemDto.Category;
                menuItem.Price = menuItemDto.Price;
                menuItem.SpecialDietaryInfo = menuItemDto.SpecialDietaryInfo;
                menuItem.TasteInfo = menuItemDto.TasteInfo;
                menuItem.NutritionalInfo = menuItemDto.NutritionalInfo;
                menuItem.AvailabilityTime = menuItemDto.AvailabilityTime;
                menuItem.IsInStock = menuItemDto.IsInStock;
                menuItem.ImageUrl = menuItemDto.ImageUrl;
                menuItem.IsAvailable = menuItemDto.IsAvailable;
                menuItem.Discounts = menuItemDto.Discounts;
                menuItem.CreatedDate = menuItemDto.CreatedDate;

                _context.MenuItems.Update(menuItem);
                _context.SaveChanges();
            }
        }

        public void DeleteMenuItem(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                _context.SaveChanges();
            }
        }



        public IEnumerable<MenuItemDTO> GetMenuItemsByCategory(string category)
        {
            return _context.MenuItems.Where(m => m.Category == category).Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                ItemName = menuItem.ItemName,
                Category = menuItem.Category,
                Price = menuItem.Price,
                TasteInfo = menuItem.TasteInfo,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                Discounts = menuItem.Discounts
            }).ToList();
        }

        public IEnumerable<MenuItemDTO> GetMenuItemsByLeastPrice()
        {
            return _context.MenuItems.OrderBy(m => m.Price).Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                ItemName = menuItem.ItemName,
                Category = menuItem.Category,
                Price = menuItem.Price,
                TasteInfo = menuItem.TasteInfo,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                Discounts = menuItem.Discounts
            }).ToList();
        }

        public IEnumerable<MenuItemDTO> GetMenuItemsByTasteInfo(string tasteInfo)
        {
            return _context.MenuItems.Where(m => m.TasteInfo == tasteInfo).Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                ItemName = menuItem.ItemName,
                Category = menuItem.Category,
                Price = menuItem.Price,
                TasteInfo = menuItem.TasteInfo,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                Discounts = menuItem.Discounts
            }).ToList();
        }

        public IEnumerable<MenuItemDTO> GetMenuItemsBySpecialDietaryInfo(string dietaryInfo)
        {
            return _context.MenuItems.Where(m => m.SpecialDietaryInfo == dietaryInfo).Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                ItemName = menuItem.ItemName,
                Category = menuItem.Category,
                Price = menuItem.Price,
                TasteInfo = menuItem.TasteInfo,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                Discounts = menuItem.Discounts
            }).ToList();
        }

        public IEnumerable<MenuItemDTO> GetMenuItemsByNutritionalInfo(string nutritionalInfo)
        {
            return _context.MenuItems.Where(m => m.NutritionalInfo == nutritionalInfo).Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                ItemName = menuItem.ItemName,
                Category = menuItem.Category,
                Price = menuItem.Price,
                TasteInfo = menuItem.TasteInfo,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                Discounts = menuItem.Discounts
            }).ToList();
        }

        public IEnumerable<MenuItemDTO> GetMenuItemsByHighDiscount()
        {
            return _context.MenuItems.OrderByDescending(m => m.Discounts).Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                ItemName = menuItem.ItemName,
                Category = menuItem.Category,
                Price = menuItem.Price,
                TasteInfo = menuItem.TasteInfo,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                NutritionalInfo = menuItem.NutritionalInfo,
                Discounts = menuItem.Discounts
            }).ToList();
        }
    }
}
