using HotpotLibrary.Interfaces;  // Import the interface
using HotpotLibrary.Models;
using System.Collections.Generic;

namespace HotpotLibrary.Service
{
    public class ReportService
    {
        private readonly IReportRepository _reportRepository; // Use the interface

        public ReportService(IReportRepository reportRepository) // Accept the interface in the constructor
        {
            _reportRepository = reportRepository;
        }

        public List<MenuItem> GetVegItemsByRestaurant(int restaurantId)
        {
            return _reportRepository.GetVegItemsByRestaurant(restaurantId);
        }

        public List<MenuItem> GetNonVegItemsByRestaurant(int restaurantId)
        {
            return _reportRepository.GetNonVegItemsByRestaurant(restaurantId);
        }

        public int GetTotalMenuItemsByRestaurant(int restaurantId)
        {
            return _reportRepository.GetTotalMenuItemsByRestaurant(restaurantId);
        }

        public int GetTotalCategoriesByRestaurant(int restaurantId)
        {
            // Assuming the _reportRepository is returning a list of categories
            var totalCategories = _reportRepository.GetTotalCategoriesByRestaurant(restaurantId);

            // Return the count of categories
            return totalCategories;
        }


        public int GetTotalOrdersByRestaurant(int restaurantId)
        {
            return _reportRepository.GetTotalOrdersByRestaurant(restaurantId);
        }

        public decimal GetTotalRevenueByRestaurant(int restaurantId)
        {
            return _reportRepository.GetTotalRevenueByRestaurant(restaurantId);
        }

        public decimal GetRevenueByRestaurant(int restaurantId, string period)
        {
            return _reportRepository.GetRevenueByRestaurant(restaurantId, period);
        }

        public decimal GetAverageOrderValueByRestaurant(int restaurantId)
        {
            return _reportRepository.GetAverageOrderValueByRestaurant(restaurantId);
        }

        public List<MenuItem> GetTopSellingItemsByRestaurant(int restaurantId, int topN)
        {
            return _reportRepository.GetTopSellingItemsByRestaurant(restaurantId, topN);
        }

        public List<MenuItem> GetLeastSellingItemsByRestaurant(int restaurantId, int bottomN)
        {
            return _reportRepository.GetLeastSellingItemsByRestaurant(restaurantId, bottomN);
        }

        public List<User> GetRegularUsersByRestaurant(int restaurantId, int minOrders)
        {
            return _reportRepository.GetRegularUsersByRestaurant(restaurantId, minOrders);
        }
    }
}
