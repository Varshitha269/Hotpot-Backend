using HotpotLibrary.Models;
using System.Collections.Generic;

namespace HotpotLibrary.Interfaces
{
    public interface IReportRepository
    {
        // For menu items and categories
        List<MenuItem> GetVegItemsByRestaurant(int restaurantId);
        List<MenuItem> GetNonVegItemsByRestaurant(int restaurantId);
        int GetTotalMenuItemsByRestaurant(int restaurantId);
        public int GetTotalCategoriesByRestaurant(int restaurantId);

        // For orders and revenue
        int GetTotalOrdersByRestaurant(int restaurantId);
        decimal GetTotalRevenueByRestaurant(int restaurantId);
        decimal GetRevenueByRestaurant(int restaurantId, string period);
        decimal GetAverageOrderValueByRestaurant(int restaurantId);

        // For selling items
        List<MenuItem> GetTopSellingItemsByRestaurant(int restaurantId, int topN);
        List<MenuItem> GetLeastSellingItemsByRestaurant(int restaurantId, int bottomN);

        // For regular users
        List<User> GetRegularUsersByRestaurant(int restaurantId, int minOrders);
    }
}
