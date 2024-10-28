using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace HotpotLibrary.Services
{
    public class UserServiceReports : IUserServiceReportsInterface
    {
        private readonly AppDbContext _context;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(UserServiceReports));

        public UserServiceReports(AppDbContext context)
        {
            _context = context;
        }

        public void RegisterUser(UserDTO userDto)
        {
            _logger.Info($"Service: Registering user with Username: {userDto.Username}");

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                PhNo = userDto.PhNo,
                AddressLine = userDto.AddressLine,
                City = userDto.City,
                State = userDto.State,
                PostalCode = userDto.PostalCode,
                Country = userDto.Country,
                Role = userDto.Role,
                CreatedDate = userDto.CreatedDate,
                IsActive = userDto.IsActive
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            _logger.Info("Service: User registered successfully.");
        }

        public UserDTO Login(string usernameOrEmail, string password)
        {
            _logger.Info($"Service: Attempting login for Username/Email: {usernameOrEmail}");

            var user = _context.Users.SingleOrDefault(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password);
            if (user == null)
            {
                _logger.Warn($"Service: Login failed for Username/Email: {usernameOrEmail}");
                return null;
            }

            _logger.Info($"Service: Login successful for Username/Email: {usernameOrEmail}");
            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                PhNo = user.PhNo,
                AddressLine = user.AddressLine,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Role = user.Role,
                CreatedDate = (DateTime)user.CreatedDate,
                IsActive = (bool)user.IsActive
            };
        }

        public void Logout()
        {
            _logger.Info("Service: Logging out user.");
            // Implement session management or token invalidation here
        }

        public UserDTO GetUserByEmail(string email)
        {
            _logger.Info($"Service: Fetching user with Email: {email}");

            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                _logger.Warn($"Service: No user found with Email: {email}");
                return null;
            }

            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                PhNo = user.PhNo,
                AddressLine = user.AddressLine,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Role = user.Role,
                CreatedDate = (DateTime)user.CreatedDate,
                IsActive = (bool)user.IsActive
            };
        }

        public IEnumerable<MenuDTO> GetAllMenus()
        {
            _logger.Info("Service: Fetching all menus.");

            var menus = _context.Menus.Select(menu => new MenuDTO
            {
                MenuID = menu.MenuID,
                RestaurantID = menu.RestaurantID,
                MenuName = menu.MenuName,
                Description = menu.Description,
                CreatedDate = (DateTime)menu.CreatedDate,
                IsActive = (bool)menu.IsActive
            }).ToList();

            _logger.Info($"Service: Retrieved {menus.Count} menus.");
            return menus;
        }

        public IEnumerable<MenuItemDTO> SearchMenuItems(string query)
        {
            _logger.Info($"Service: Searching menu items with query: {query}");

            var items = _context.MenuItems
                .Where(item => item.ItemName.Contains(query) || item.Description.Contains(query))
                .Select(item => new MenuItemDTO
                {
                    MenuItemID = item.MenuItemID,
                    MenuID = item.MenuID,
                    ItemName = item.ItemName,
                    Description = item.Description,
                    Category = item.Category,
                    Price = item.Price,
                    SpecialDietaryInfo = item.SpecialDietaryInfo,
                    TasteInfo = item.TasteInfo,
                    NutritionalInfo = item.NutritionalInfo,
                    AvailabilityTime = item.AvailabilityTime,
                    IsInStock = (bool)item.IsInStock,
                    ImageUrl = item.ImageUrl,
                    IsAvailable = (bool)item.IsAvailable,
                    Discounts = (decimal)item.Discounts,
                    CreatedDate = (DateTime)item.CreatedDate
                }).ToList();

            _logger.Info($"Service: Found {items.Count} menu items matching query.");
            return items;
        }

        public MenuItemDTO GetMenuItemById(int id)
        {
            _logger.Info($"Service: Fetching menu item with ID: {id}");

            var item = _context.MenuItems.Find(id);
            if (item == null)
            {
                _logger.Warn($"Service: No menu item found with ID: {id}");
                return null;
            }

            return new MenuItemDTO
            {
                MenuItemID = item.MenuItemID,
                MenuID = item.MenuID,
                ItemName = item.ItemName,
                Description = item.Description,
                Category = item.Category,
                Price = item.Price,
                SpecialDietaryInfo = item.SpecialDietaryInfo,
                TasteInfo = item.TasteInfo,
                NutritionalInfo = item.NutritionalInfo,
                AvailabilityTime = item.AvailabilityTime,
                IsInStock = (bool)item.IsInStock,
                ImageUrl = item.ImageUrl,
                IsAvailable = (bool)item.IsAvailable,
                Discounts = (decimal)item.Discounts,
                CreatedDate = (DateTime)item.CreatedDate
            };
        }

        public CartDTO GetCartContents(int userId)
        {
            _logger.Info($"Service: Fetching cart contents for user with ID: {userId}");

            var cartItems = _context.Carts.Where(c => c.UserID == userId).ToList();
            var totalCost = cartItems.Sum(c => c.Price * c.Quantity);

            _logger.Info($"Service: Found {cartItems.Count} items in cart for user ID: {userId}");
            return new CartDTO
            {
                CartID = 0,
                UserID = userId,
                MenuItemID = 0,
                Quantity = cartItems.Sum(c => c.Quantity),
                Price = totalCost,
                CreatedDate = DateTime.Now
            };
        }

        public OrderDTO GetOrderDetails(int orderId)
        {
            _logger.Info($"Service: Fetching order details for order ID: {orderId}");

            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                _logger.Warn($"Service: No order found with ID: {orderId}");
                return null;
            }

            return new OrderDTO
            {
                UserID = order.UserID,
                RestaurantID = order.RestaurantID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryDate = order.DeliveryDate
            };
        }

        public IEnumerable<OrderDTO> GetOrderHistory(int userId)
        {
            _logger.Info($"Service: Fetching order history for user ID: {userId}");

            var orders = _context.Orders.Where(o => o.UserID == userId).Select(order => new OrderDTO
            {
                UserID = order.UserID,
                RestaurantID = order.RestaurantID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryDate = order.DeliveryDate
            }).ToList();

            _logger.Info($"Service: Retrieved {orders.Count} orders for user ID: {userId}");
            return orders;
        }
    }
}
