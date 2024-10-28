using HotpotLibrary.Models;


namespace HotpotLibrary.Interfaces
{
    public  interface IAdminDashboard
    {
        List<Order> GetOrdersFromLast7Days();
        List<User> GetRegularUsers();
        Dictionary<DateTime, int> GetTotalOrdersPerDay(DateTime startDate, DateTime endDate);
        Dictionary<string, Dictionary<DateTime, int>> GetMenuItemCountsByCategory(string category, DateTime startDate, DateTime endDate);
        Dictionary<DateTime, decimal> GetRevenueReports(string period);

        List<MenuItem> GetTopSellingItems(string period);

        List<User> GetUsersByOrderFrequency(string period);

        Dictionary<DateTime, decimal> GetAverageOrderValue(string period);

        Dictionary<string, int> GetVegNonVegItemCount(int restaurantId);


    }
}
