using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class OrderDetailsRepository : IOrderDetailInterface
    {
        private readonly AppDbContext _context;

        public OrderDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public OrderDetailDTO GetOrderDetailsById(int id)
        {
            var orderDetails = _context.OrderDetails.Find(id);
            if (orderDetails == null) return null;

            return new OrderDetailDTO
            {
                OrderID = orderDetails.OrderID,
                MenuItemID = orderDetails.MenuItemID,
                Quantity = orderDetails.Quantity,
                Price = orderDetails.Price,
                Subtotal = (decimal)orderDetails.Subtotal
            };
        }

        public IEnumerable<OrderDetailDTO> GetAllOrderDetails()
        {
            return _context.OrderDetails.Select(orderDetails => new OrderDetailDTO
            {
                OrderID = orderDetails.OrderID,
                MenuItemID = orderDetails.MenuItemID,
                Quantity = orderDetails.Quantity,
                Price = orderDetails.Price,
                Subtotal = (decimal)orderDetails.Subtotal
            }).ToList();
        }

        public void CreateOrderDetails(OrderDetailDTO OrderDetailDTO)
        {
            var orderDetails = new OrderDetail
            {
                OrderID = OrderDetailDTO.OrderID,
                MenuItemID = OrderDetailDTO.MenuItemID,
                Quantity = OrderDetailDTO.Quantity,
                Price = OrderDetailDTO.Price
            };
            _context.OrderDetails.Add(orderDetails);
            _context.SaveChanges();
        }

        public void UpdateOrderDetails(int id, OrderDetailDTO OrderDetailDTO)
        {
            var orderDetails = _context.OrderDetails.Find(id);
            if (orderDetails != null)
            {
                orderDetails.OrderID = OrderDetailDTO.OrderID;
                orderDetails.MenuItemID = OrderDetailDTO.MenuItemID;
                orderDetails.Quantity = OrderDetailDTO.Quantity;
                orderDetails.Price = OrderDetailDTO.Price;

                _context.OrderDetails.Update(orderDetails);
                _context.SaveChanges();
            }
        }

        public void DeleteOrderDetails(int id)
        {
            var orderDetails = _context.OrderDetails.Find(id);
            if (orderDetails != null)
            {
                _context.OrderDetails.Remove(orderDetails);
                _context.SaveChanges();
            }
        }
    }
}