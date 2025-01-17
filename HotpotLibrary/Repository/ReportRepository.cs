﻿using HotpotLibrary.Data;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get vegetarian items for a specific restaurant
        public List<MenuItem> GetVegItemsByRestaurant(int restaurantId)
        {
            var vegItems = _context.MenuItems
                .Join(_context.Menus, menuItem => menuItem.MenuID, menu => menu.MenuID,
                (menuItem, menu) => new { menuItem, menu })
                .Where(m => m.menu.RestaurantID == restaurantId && m.menuItem.SpecialDietaryInfo == "Veg")
                .Select(m => m.menuItem)
                .ToList();

            return vegItems;
        }

        // Get non-vegetarian items for a specific restaurant
        public List<MenuItem> GetNonVegItemsByRestaurant(int restaurantId)
        {
            var nonVegItems = _context.MenuItems
                .Join(_context.Menus, menuItem => menuItem.MenuID, menu => menu.MenuID,
                (menuItem, menu) => new { menuItem, menu })
                .Where(m => m.menu.RestaurantID == restaurantId && m.menuItem.SpecialDietaryInfo == "Non-Veg")
                .Select(m => m.menuItem)
                .ToList();

            return nonVegItems;
        }

        // Get total menu items available for a restaurant
        public int GetTotalMenuItemsByRestaurant(int restaurantId)
        {
            var totalMenuItems = _context.MenuItems
                .Join(_context.Menus, menuItem => menuItem.MenuID, menu => menu.MenuID,
                (menuItem, menu) => new { menuItem, menu })
                .Where(m => m.menu.RestaurantID == restaurantId && (m.menuItem.IsInStock ?? false)) // Adjusted here
                .Count();

            return totalMenuItems;
        }

        // Get total distinct categories of items for a restaurant
        public int GetTotalCategoriesByRestaurant(int restaurantId)
        {
            var totalCategoriesCount = _context.MenuItems
                .Join(_context.Menus, menuItem => menuItem.MenuID, menu => menu.MenuID,
                (menuItem, menu) => new { menuItem, menu })
                .Where(m => m.menu.RestaurantID == restaurantId)
                .Select(m => m.menuItem.Category)
                .Distinct()
                .Count(); // Get the count directly

            return totalCategoriesCount;
        }


        // Get total orders placed at a restaurant
        public int GetTotalOrdersByRestaurant(int restaurantId)
        {
            var totalOrders = _context.Orders
                .Where(order => order.RestaurantID == restaurantId)
                .Count();

            return totalOrders;
        }

        // Get total revenue generated by a restaurant
        public decimal GetTotalRevenueByRestaurant(int restaurantId)
        {
            var totalRevenue = _context.Orders
                .Where(order => order.RestaurantID == restaurantId)
                .Sum(order => order.TotalAmount);

            return totalRevenue;
        }

        // Get revenue by restaurant for a specific period (daily, monthly, yearly)
        public decimal GetRevenueByRestaurant(int restaurantId, string period)
        {
            var query = _context.Orders
                .Where(order => order.RestaurantID == restaurantId);

            switch (period.ToLower())
            {
                case "daily":
                    query = query.Where(o => o.OrderDate.Date == DateTime.Today);
                    break;
                case "monthly":
                    query = query.Where(o => o.OrderDate.Month == DateTime.Now.Month && o.OrderDate.Year == DateTime.Now.Year);
                    break;
                case "yearly":
                    query = query.Where(o => o.OrderDate.Year == DateTime.Now.Year);
                    break;
            }

            return query.Sum(order => order.TotalAmount);
        }

        // Get average order value for a restaurant
        public decimal GetAverageOrderValueByRestaurant(int restaurantId)
        {
            var avgOrderValue = _context.Orders
                .Where(order => order.RestaurantID == restaurantId)
                .Average(order => order.TotalAmount);

            return avgOrderValue;
        }

        // Get top N selling items for a restaurant
        public List<MenuItem> GetTopSellingItemsByRestaurant(int restaurantId, int topN)
        {
            var topSellingItems = _context.OrderDetails
                .Join(_context.MenuItems, orderDetail => orderDetail.MenuItemID, menuItem => menuItem.MenuItemID,
                (orderDetail, menuItem) => new { orderDetail, menuItem })
                .Join(_context.Menus, combined => combined.menuItem.MenuID, menu => menu.MenuID,
                (combined, menu) => new { combined.orderDetail, combined.menuItem, menu })
                .Where(m => m.menu.RestaurantID == restaurantId)
                .GroupBy(m => new
                {
                    MenuItemID = m.menuItem.MenuItemID,
                    ItemName = m.menuItem.ItemName
                })
                .Select(g => new
                {
                    g.Key.MenuItemID,
                    g.Key.ItemName,
                    TotalSold = g.Sum(x => x.orderDetail.Quantity) // Calculate total sold quantity
                })
                .OrderByDescending(x => x.TotalSold) // Order by total sold quantity descending
                .Take(topN)
                .Select(x => new MenuItem
                {
                    MenuItemID = x.MenuItemID,
                    ItemName = x.ItemName
                    // Include other properties you need from MenuItem here
                })
                .ToList();

            return topSellingItems;
        }


        // Get bottom N selling items for a restaurant
        public List<MenuItem> GetLeastSellingItemsByRestaurant(int restaurantId, int bottomN)
        {
            var leastSellingItems = _context.MenuItems // Start from MenuItems to include unsold items
                .Where(menuItem => menuItem.Menu.RestaurantID == restaurantId) // Filter by restaurant
                .GroupJoin(_context.OrderDetails,
                    menuItem => menuItem.MenuItemID,
                    orderDetail => orderDetail.MenuItemID,
                    (menuItem, orderDetails) => new
                    {
                        MenuItem = menuItem,
                        TotalSold = orderDetails.Sum(od => od.Quantity) // Calculate total sold quantity
                    })
                .OrderBy(x => x.TotalSold) // Order by total sold quantity ascending
                .Take(bottomN)
                .Select(x => new MenuItem
                {
                    MenuItemID = x.MenuItem.MenuItemID,
                    ItemName = x.MenuItem.ItemName
                    // Include other properties you need from MenuItem here
                })
                .ToList();

            return leastSellingItems;
        }


        // Get regular users who have placed more than a specified number of orders at a restaurant
        public List<User> GetRegularUsersByRestaurant(int restaurantId, int minOrders)
        {
            var regularUsers = _context.Orders
                .Where(order => order.RestaurantID == restaurantId)
                .GroupBy(order => order.UserID)
                .Where(g => g.Count() >= minOrders)
                .Select(g => g.Key)
                .Join(_context.Users, userId => userId, user => user.UserID, (userId, user) => user)
                .ToList();

            return regularUsers;
        }
    }
}
