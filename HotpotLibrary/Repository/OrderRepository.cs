using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class OrderRepository : IOrderInterface
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public OrderDTO GetOrderById(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return null;

            return new OrderDTO
            {
                OrderID= order.OrderID,
                UserID = order.UserID,
                RestaurantID = order.RestaurantID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryDate = order.DeliveryDate,
                CreatedDate = order.CreatedDate
            };
        }

        public List<OrderDTO> GetOrdersByUserId(int userId)
        {
            var orders = _context.Orders
                .Where(o => o.UserID == userId)
                .Select(o => new OrderDTO
                {
                    OrderID = o.OrderID,
                    UserID = o.UserID,
                    RestaurantID = o.RestaurantID,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderStatus = o.OrderStatus,
                    PaymentStatus = o.PaymentStatus,
                    DeliveryAddress = o.DeliveryAddress,
                    DeliveryDate = o.DeliveryDate
                }).ToList();

            return orders;
        }

        public List<OrderDTO> GetOrdersByRestaurantId(int restaurantId)
        {
            var orders = _context.Orders
                .Where(o => o.RestaurantID == restaurantId)
                .Select(o => new OrderDTO
                {
                    OrderID = o.OrderID,
                    UserID = o.UserID,
                    RestaurantID = o.RestaurantID,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderStatus = o.OrderStatus,
                    PaymentStatus = o.PaymentStatus,
                    DeliveryAddress = o.DeliveryAddress,
                    DeliveryDate = o.DeliveryDate,
                    CreatedDate = o.CreatedDate
                }).ToList();

            return orders;
        }



        public IEnumerable<OrderDTO> GetAllOrders()
        {
            return _context.Orders.Select(order => new OrderDTO
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                RestaurantID = order.RestaurantID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryDate = order.DeliveryDate,
                CreatedDate = order.CreatedDate
            }).ToList();
        }

        public void CreateOrder(OrderDTO orderDto)
        {
            var order = new Order
            {

                OrderID = orderDto.OrderID,
                UserID = orderDto.UserID,
                RestaurantID = orderDto.RestaurantID,
                OrderDate = DateTime.Now,
                TotalAmount = (decimal)orderDto.TotalAmount,
                OrderStatus = orderDto.OrderStatus,
                PaymentStatus = orderDto.PaymentStatus,
                DeliveryAddress = orderDto.DeliveryAddress,
                DeliveryDate = (DateTime)orderDto.DeliveryDate,
                CreatedDate = DateTime.Now
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
          orderDto.OrderID = order.OrderID;
        }

        public void UpdateOrder(int id, OrderDTO orderDto)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                order.UserID = orderDto.UserID;
                order.RestaurantID = orderDto.RestaurantID;
                order.OrderDate = (DateTime)orderDto.OrderDate;
                order.TotalAmount = (decimal)orderDto.TotalAmount;
                order.OrderStatus = orderDto.OrderStatus;
                order.PaymentStatus = orderDto.PaymentStatus;
                order.DeliveryAddress = orderDto.DeliveryAddress;
                order.DeliveryDate = (DateTime)orderDto.DeliveryDate;

                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }


        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
