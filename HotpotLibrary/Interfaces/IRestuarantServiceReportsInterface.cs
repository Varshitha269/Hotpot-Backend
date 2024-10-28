using HotpotLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IRestaurantServiceReportsInterface
    {
        void Login(string username, string password);
        void Logout();
        void CreateMenuItem(MenuItemDTO menuItemDto);
        void UpdateMenuItem(MenuItemDTO menuItemDto);
        void DeleteMenuItem(int menuItemId);
        void MarkMenuItemOutOfStock(int menuItemId);
     
        List<OrderDTO> GetOngoingOrders(int restaurantId);
        void UpdateOrderStatus(int orderId, string orderStatus);
        List<OrderDTO> GetOrderHistory(int restaurantId);
        List<MenuItemDTO> GetMenuItems(int restaurantId);
    }

}
