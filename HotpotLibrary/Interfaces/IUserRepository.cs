using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUserById(int UserId);
        void CreateUser(UserDTO userDto);
        void UpdateUser(int id, UserDTO userDto);
        void DeleteUser(int UserId);

        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        IEnumerable<FeedbackRating> GetUserReviews(int userId);
        IEnumerable<Order> GetAllOrdersByUserId(int userId);
    }
}
