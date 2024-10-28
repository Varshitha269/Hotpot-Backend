using HotpotLibrary.Data;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Services
{
    public class ReportsService : IReportsService
    {
        private readonly AppDbContext _context;

        public ReportsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderStatisticsReport> GetOrderStatisticsAsync()
        {
            return new OrderStatisticsReport
            {
                Received = await _context.Orders.CountAsync(),
                Delivered = await _context.Orders.CountAsync(o => o.OrderStatus == "delivered"),
                Cancelled = await _context.Orders.CountAsync(o => o.OrderStatus == "cancelled"),
                Processing = await _context.Orders.CountAsync(o => o.OrderStatus == "preparing"),

                Ontheway = await _context.Orders.CountAsync(o => o.OrderStatus == "on the way"),
                Confirmed = await _context.Orders.CountAsync(o => o.OrderStatus == "confirmed"),


            };
        }

        public async Task<RestaurantStatisticsReport> GetRestaurantStatisticsAsync()
        {
            return new RestaurantStatisticsReport
            {
                ActiveRestaurants = await _context.Restaurants.CountAsync(r => r.IsActive),
                InactiveRestaurants = await _context.Restaurants.CountAsync(r => !r.IsActive)
            };
        }

        public async Task<UserStatisticsReport> GetUserStatisticsAsync()
        {
            return new UserStatisticsReport
            {
                ActiveUsers = await _context.Users.CountAsync(u => u.IsActive),
                InactiveUsers = await _context.Users.CountAsync(u => !u.IsActive)
            };
        }

        //public async Task<MenuStatisticsReport> GetMenuStatisticsAsync()
        //{
        //    return new MenuStatisticsReport
        //    {
        //        ActiveMenus = await _context.Menus.CountAsync(m => m.IsActive = true),
        //        InactiveMenus = await _context.Menus.CountAsync(m => !m.IsActive)
        //    };
        //}

        public async Task<PaymentStatisticsReport> GetPaymentStatisticsAsync()
        {
            return new PaymentStatisticsReport
            {
                CompletedPayments = await _context.Payments.CountAsync(p => p.TransactionStatus == "Completed"),
                FailedPayments = await _context.Payments.CountAsync(p => p.TransactionStatus == "Failed"),
                PendingPayments = await _context.Payments.CountAsync(p => p.TransactionStatus == "Pending")
            };
        }

    }
}
