using System.Collections.Generic;
using System.Linq;
using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using log4net;

public class RestaurantServiceReports : IRestaurantServiceReportsInterface
{
    private readonly AppDbContext _context;
    private static readonly ILog _logger = LogManager.GetLogger(typeof(RestaurantServiceReports));

    public RestaurantServiceReports(AppDbContext context)
    {
        _context = context;
    }

    public void Login(string username, string password)
    {
        // Implementation for login
        _logger.Info($"Restaurant manager logged in with username: {username}");
    }

    public void Logout()
    {
        // Implementation for logout
        _logger.Info("Restaurant manager logged out.");
    }

    public void CreateMenuItem(MenuItemDTO menuItemDto)
    {
        _logger.Info($"Creating menu item with details: {menuItemDto}");
        var menuItem = new MenuItem
        {
            ItemName = menuItemDto.ItemName,
            Description = menuItemDto.Description,
            Price = menuItemDto.Price,
            AvailabilityTime = menuItemDto.AvailabilityTime,
            SpecialDietaryInfo = menuItemDto.SpecialDietaryInfo,
            //CategoryId = menuItemDto.CategoryId
        };
        _context.MenuItems.Add(menuItem);
        _context.SaveChanges();
        _logger.Info("Menu item created successfully.");
    }

    public void UpdateMenuItem(MenuItemDTO menuItemDto)
    {
        _logger.Info($"Updating menu item with ID: {menuItemDto.MenuID}");
        var menuItem = _context.MenuItems.Find(menuItemDto.MenuID);
        if (menuItem != null)
        {
            menuItem.ItemName = menuItemDto.ItemName;
            menuItem.Description = menuItemDto.Description;
            menuItem.Price = menuItemDto.Price;
            menuItem.AvailabilityTime = menuItemDto.AvailabilityTime;
            menuItem.SpecialDietaryInfo = menuItemDto.SpecialDietaryInfo;
            // menuItem.CategoryId = menuItemDto.CategoryId;
            _context.SaveChanges();
            _logger.Info("Menu item updated successfully.");
        }
        else
        {
            _logger.Warn("Menu item not found.");
        }
    }

    public void DeleteMenuItem(int menuItemId)
    {
        _logger.Info($"Deleting menu item with ID: {menuItemId}");
        var menuItem = _context.MenuItems.Find(menuItemId);
        if (menuItem != null)
        {
            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();
            _logger.Info("Menu item deleted successfully.");
        }
        else
        {
            _logger.Warn("Menu item not found.");
        }
    }

    public void MarkMenuItemOutOfStock(int menuItemId)
    {
        _logger.Info($"Marking menu item with ID: {menuItemId} as out of stock.");
        var menuItem = _context.MenuItems.Find(menuItemId);
        if (menuItem != null)
        {
            menuItem.IsInStock = false;
            _context.SaveChanges();
            _logger.Info("Menu item marked as out of stock.");
        }
        else
        {
            _logger.Warn("Menu item not found.");
        }
    }

    public List<OrderDTO> GetOngoingOrders(int restaurantId)
    {
        _logger.Info($"Retrieving ongoing orders for restaurant ID: {restaurantId}");
        var orders = _context.Orders
            .Where(o => o.RestaurantID == restaurantId && o.OrderStatus == "Ongoing")
            .Select(o => new OrderDTO
            {
                OrderID = o.OrderID,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus
            })
            .ToList();
        _logger.Info("Retrieved ongoing orders.");
        return orders;
    }

    public void UpdateOrderStatus(int orderId, string orderStatus)
    {
        _logger.Info($"Updating status of order ID: {orderId} to {orderStatus}");
        var order = _context.Orders.Find(orderId);
        if (order != null)
        {
            order.OrderStatus = orderStatus;
            _context.SaveChanges();
            _logger.Info("Order status updated successfully.");
        }
        else
        {
            _logger.Warn("Order not found.");
        }
    }

    public List<OrderDTO> GetOrderHistory(int restaurantId)
    {
        _logger.Info($"Retrieving order history for restaurant ID: {restaurantId}");
        var orders = _context.Orders
            .Where(o => o.RestaurantID == restaurantId)
            .Select(o => new OrderDTO
            {
                OrderID = o.OrderID,
                TotalAmount = o.TotalAmount,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus
            })
            .ToList();
        _logger.Info("Retrieved order history.");
        return orders;
    }

    public List<MenuItemDTO> GetMenuItems(int restaurantId)
    {
        _logger.Info($"Retrieving menu items for restaurant ID: {restaurantId}");

        // Retrieve MenuIDs associated with the given restaurantId
        var menuIds = _context.Menus
            .Where(menu => menu.RestaurantID == restaurantId)
            .Select(menu => menu.MenuID)
            .ToList();

        if (!menuIds.Any())
        {
            _logger.Info("No menus found for the given restaurant ID.");
            return new List<MenuItemDTO>();
        }

        // Retrieve MenuItems based on the MenuIDs
        var menuItems = _context.MenuItems
            .Where(menuItem => menuIds.Contains(menuItem.MenuID))
            .Select(menuItem => new MenuItemDTO
            {
                MenuItemID = menuItem.MenuItemID,
                ItemName = menuItem.ItemName,
                Description = menuItem.Description,
                Price = menuItem.Price,
                AvailabilityTime = menuItem.AvailabilityTime,
                SpecialDietaryInfo = menuItem.SpecialDietaryInfo,
                // Uncomment and set CategoryId if needed
                // CategoryId = menuItem.CategoryId
            })
            .ToList();

        _logger.Info("Retrieved menu items.");
        return menuItems;
    }
}
