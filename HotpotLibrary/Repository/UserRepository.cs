using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public UserDTO GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return null;

            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                PhNo = user.PhNo,
                AddressLine = user.AddressLine,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Role = user.Role,
                CreatedDate = (DateTime)user.CreatedDate,
                IsActive = (bool)user.IsActive
            };
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _context.Users.Select(user => new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                PhNo = user.PhNo,
                AddressLine = user.AddressLine,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Role = user.Role,
                CreatedDate = (DateTime)user.CreatedDate,
                IsActive = (bool)user.IsActive
            }).ToList();
        }

        public void CreateUser(UserDTO userDto)
        {
            var user = new User
            {
                UserID = userDto.UserID,
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                PhNo = userDto.PhNo,
                AddressLine = userDto.AddressLine,
                City = userDto.City,
                State = userDto.State,
                PostalCode = userDto.PostalCode,
                Country = userDto.Country,
                Role = userDto.Role,
                CreatedDate = userDto.CreatedDate,
                IsActive = userDto.IsActive
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(int id, UserDTO userDto)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.Username = userDto.Username;
                user.Email = userDto.Email;
                user.Password = userDto.Password;
                user.PhNo = userDto.PhNo;
                user.AddressLine = userDto.AddressLine;
                user.City = userDto.City;
                user.State = userDto.State;
                user.PostalCode = userDto.PostalCode;
                user.Country = userDto.Country;
                user.Role = userDto.Role;
                user.CreatedDate = userDto.CreatedDate;
                user.IsActive = userDto.IsActive;

                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<FeedbackRating> GetUserReviews(int userId)
        {
            return _context.FeedbackRatings.Where(fr => fr.UserID == userId).ToList();
        }

        public IEnumerable<Order> GetAllOrdersByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserID == userId).ToList();
        }
    }
}
