using HotpotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IReportsService
    {
        Task<OrderStatisticsReport> GetOrderStatisticsAsync();
        Task<RestaurantStatisticsReport> GetRestaurantStatisticsAsync();
        Task<UserStatisticsReport> GetUserStatisticsAsync();
        //Task<MenuStatisticsReport> GetMenuStatisticsAsync();
        Task<PaymentStatisticsReport> GetPaymentStatisticsAsync();
    }
}
