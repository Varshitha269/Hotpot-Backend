using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IOrderDetailInterface
    {
        OrderDetailDTO GetOrderDetailsById(int id);
        IEnumerable<OrderDetailDTO> GetAllOrderDetails();
        void CreateOrderDetails(OrderDetailDTO OrderDetailDTO);
        void UpdateOrderDetails(int id, OrderDetailDTO OrderDetailDTO);
        void DeleteOrderDetails(int id);
    }
}
