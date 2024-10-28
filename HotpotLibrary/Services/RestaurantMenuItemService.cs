using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using log4net; // Ensure you have log4net referenced
using System;
using System.Collections.Generic;

namespace HotpotLibrary.Services
{
    public class RestaurantMenuItemService
    {
        private readonly IRestaurantMenuItem _restaurantMenuItemRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(RestaurantMenuItemService));

        public RestaurantMenuItemService(IRestaurantMenuItem restaurantMenuItemRepository)
        {
            _restaurantMenuItemRepository = restaurantMenuItemRepository;
        }

        // Method to get restaurants with a specific menu item
        public IEnumerable<RestaurantMenuItem> GetRestaurantsWithMenuItem(string menuItemName)
        {
            try
            {
                _logger.Info($"Fetching restaurants with menu item: {menuItemName}");
                var restaurants = _restaurantMenuItemRepository.GetRestaurantsWithMenuItem(menuItemName);

                // Optionally map to DTOs if needed
                // return restaurants.Select(r => new MenuDTO { ... }).ToList();

                return restaurants; // Return the list directly if no mapping is needed
            }
            catch (Exception ex)
            {
                _logger.Error("Error fetching restaurants with menu item.", ex);
                throw; // Rethrow the exception after logging
            }
        }
    }
}
