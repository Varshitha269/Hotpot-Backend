using HotpotLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IUserServiceReportsInterface
    {
            void RegisterUser(UserDTO userDto);
            UserDTO Login(string usernameOrEmail, string password);
            void Logout();
            UserDTO GetUserByEmail(string email);
            IEnumerable<MenuDTO> GetAllMenus();
            IEnumerable<MenuItemDTO> SearchMenuItems(string query);
            MenuItemDTO GetMenuItemById(int id);

            CartDTO GetCartContents(int userId);
            OrderDTO GetOrderDetails(int orderId);
            IEnumerable<OrderDTO> GetOrderHistory(int userId);
        
    }

}

