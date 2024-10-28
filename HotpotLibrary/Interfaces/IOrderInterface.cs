using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IOrderInterface
    {
        OrderDTO GetOrderById(int id);

        public List<OrderDTO> GetOrdersByRestaurantId(int restaurantId);

        public List<OrderDTO> GetOrdersByUserId(int userId);

        public void UpdateOrderStatus(int orderId, string status);
        IEnumerable<OrderDTO> GetAllOrders();
        // void AddOrder(OrderDTO orderDto);
        void CreateOrder(OrderDTO orderDto);
        void UpdateOrder(int id, OrderDTO orderDto);
        void DeleteOrder(int id);
    }

}
