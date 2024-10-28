using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Models
{
    public class OrderStatisticsReport
    {
        public int Received { get; set; }
        public int Delivered { get; set; }
        public int Cancelled { get; set; }
        public int Processing { get; set; }
        public int Ontheway { get; set; }
        public int Confirmed { get; set; }
    }

    public class RestaurantStatisticsReport
    {
        public int ActiveRestaurants { get; set; }
        public int InactiveRestaurants { get; set; }
    }

    public class UserStatisticsReport
    {
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
    }

    public class MenuStatisticsReport
    {
        public bool ActiveMenus { get; set; }
        public bool InactiveMenus { get; set; }
    }

    public class PaymentStatisticsReport
    {
        public int CompletedPayments { get; set; }
        public int FailedPayments { get; set; }
        public int PendingPayments { get; set; }
    }
}
