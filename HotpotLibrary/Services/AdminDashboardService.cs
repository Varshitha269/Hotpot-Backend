using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using HotpotLibrary.Repository;
using System;
using System.Collections.Generic;

namespace HotpotLibrary.Services
{
    public class AdminDashboardService
    {
        private readonly IAdminDashboard _adminDashboard;

        public AdminDashboardService(IAdminDashboard adminDashboard)
        {
            _adminDashboard = adminDashboard;
        }

        public List<Order> GetOrdersFromLast7Days()
        {
            return _adminDashboard.GetOrdersFromLast7Days();
        }

        public List<User> GetRegularUsers()
        {
            return _adminDashboard.GetRegularUsers();
        }

        public Dictionary<DateTime, int> GetTotalOrdersPerDay(DateTime startDate, DateTime endDate)
        {
            return _adminDashboard.GetTotalOrdersPerDay(startDate, endDate);
        }

        public Dictionary<string, Dictionary<DateTime, int>> GetMenuItemCountsByCategory(string category, DateTime startDate, DateTime endDate)
        {
            return _adminDashboard.GetMenuItemCountsByCategory(category, startDate, endDate);
        }

        public Dictionary<DateTime, decimal> GetRevenueReports(string period)
        {
            return _adminDashboard.GetRevenueReports(period);
        }

        public List<MenuItem> GetTopSellingItems(string period)
        {
            return _adminDashboard.GetTopSellingItems(period);
        }

        public List<User> GetUsersByOrderFrequency(string period)
        {
            return _adminDashboard.GetUsersByOrderFrequency(period);
        }

        public Dictionary<DateTime, decimal> GetAverageOrderValue(string period)
        {
            return _adminDashboard.GetAverageOrderValue(period);
        }

        public Dictionary<string, int> GetVegNonVegItemCount(int restaurantId)
        {
            return _adminDashboard.GetVegNonVegItemCount(restaurantId);
        }
    }
}
