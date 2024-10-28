using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _context;

        public RestaurantRepository(AppDbContext context)
        {
            _context = context;
        }

        public RestaurantDTO GetRestaurantById(int id)
        {
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant == null) return null;

            return new RestaurantDTO
            {
                RestaurantID = restaurant.RestaurantID,
                Name = restaurant.Name,
                Description = restaurant.Description,
                PhNo = restaurant.PhNo,
                Email = restaurant.Email,
                OperatingHours = restaurant.OperatingHours,
                AddressLine = restaurant.AddressLine,
                City = restaurant.City,
                State = restaurant.State,
                PostalCode = restaurant.PostalCode,
                Country = restaurant.Country,
                CreatedDate = (DateTime)restaurant.CreatedDate,
                IsActive = (bool)restaurant.IsActive
            };
        }

        public IEnumerable<RestaurantDTO> GetAllRestaurants()
        {
            return _context.Restaurants.Select(restaurant => new RestaurantDTO
            {
                RestaurantID = restaurant.RestaurantID,
                Name = restaurant.Name,
                Description = restaurant.Description,
                PhNo = restaurant.PhNo,
                Email = restaurant.Email,
                OperatingHours = restaurant.OperatingHours,
                AddressLine = restaurant.AddressLine,
                City = restaurant.City,
                State = restaurant.State,
                PostalCode = restaurant.PostalCode,
                Country = restaurant.Country,
                CreatedDate = (DateTime)restaurant.CreatedDate,
                IsActive = (bool)restaurant.IsActive
            }).ToList();
        }

        public void CreateRestaurant(RestaurantDTO restaurantDto)
        {
            var restaurant = new Restaurant
            {
                RestaurantID = restaurantDto.RestaurantID,
                Name = restaurantDto.Name,
                Description = restaurantDto.Description,
                PhNo = restaurantDto.PhNo,
                Email = restaurantDto.Email,
                OperatingHours = restaurantDto.OperatingHours,
                AddressLine = restaurantDto.AddressLine,
                City = restaurantDto.City,
                State = restaurantDto.State,
                PostalCode = restaurantDto.PostalCode,
                Country = restaurantDto.Country,
                CreatedDate = restaurantDto.CreatedDate,
                IsActive = restaurantDto.IsActive
            };
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public void UpdateRestaurant(int id, RestaurantDTO restaurantDto)
        {
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant != null)
            {
                restaurant.Name = restaurantDto.Name;
                restaurant.Description = restaurantDto.Description;
                restaurant.PhNo = restaurantDto.PhNo;
                restaurant.Email = restaurantDto.Email;
                restaurant.OperatingHours = restaurantDto.OperatingHours;
                restaurant.AddressLine = restaurantDto.AddressLine;
                restaurant.City = restaurantDto.City;
                restaurant.State = restaurantDto.State;
                restaurant.PostalCode = restaurantDto.PostalCode;
                restaurant.Country = restaurantDto.Country;
                restaurant.CreatedDate = restaurantDto.CreatedDate;
                restaurant.IsActive = restaurantDto.IsActive;

                _context.Restaurants.Update(restaurant);
                _context.SaveChanges();
            }
        }

        public void DeleteRestaurant(int id)
        {
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                _context.SaveChanges();
            }
        }
        public RestaurantDTO GetRestaurantByNameAndLocation(string name, string location)
        {
            var restaurant = _context.Restaurants
                .FirstOrDefault(r => r.Name.ToLower() == name.ToLower() && r.City.ToLower() == location.ToLower());

            if (restaurant == null) return null;

            return new RestaurantDTO
            {
                RestaurantID = restaurant.RestaurantID,
                Name = restaurant.Name,
                Description = restaurant.Description,
                PhNo = restaurant.PhNo,
                Email = restaurant.Email,
                OperatingHours = restaurant.OperatingHours,
                AddressLine = restaurant.AddressLine,
                City = restaurant.City,
                State = restaurant.State,
                PostalCode = restaurant.PostalCode,
                Country = restaurant.Country,
                CreatedDate = restaurant.CreatedDate,
                IsActive = restaurant.IsActive
            };
        }


        public RestaurantDTO GetRestaurantByEmail(string email)
        {
            var restaurant = _context.Restaurants
                .FirstOrDefault(r => r.Email == email);

            if (restaurant == null) return null;

            return new RestaurantDTO
            {
                RestaurantID = restaurant.RestaurantID,
                Name = restaurant.Name,
                Description = restaurant.Description,
                PhNo = restaurant.PhNo,
                Email = restaurant.Email,
                OperatingHours = restaurant.OperatingHours,
                AddressLine = restaurant.AddressLine,
                City = restaurant.City,
                State = restaurant.State,
                PostalCode = restaurant.PostalCode,
                Country = restaurant.Country,
                CreatedDate = (DateTime)restaurant.CreatedDate,
                IsActive = (bool)restaurant.IsActive
            };
        }

        public int GetRestaurantIdByName(string restaurantName)
        {
            var restaurant = _context.Restaurants
                .FirstOrDefault(r => r.Name == restaurantName);

            return restaurant.RestaurantID;
        }

        public IEnumerable<RestaurantDTO> GetAllRestaurantsByCity(string city)
        {
            return _context.Restaurants
                .Where(r => r.City == city)
                .Select(restaurant => new RestaurantDTO
                {
                    RestaurantID = restaurant.RestaurantID,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    PhNo = restaurant.PhNo,
                    Email = restaurant.Email,
                    OperatingHours = restaurant.OperatingHours,
                    AddressLine = restaurant.AddressLine,
                    City = restaurant.City,
                    State = restaurant.State,
                    PostalCode = restaurant.PostalCode,
                    Country = restaurant.Country,
                    CreatedDate = (DateTime)restaurant.CreatedDate,
                    IsActive = (bool)restaurant.IsActive
                }).ToList();
        }

    }
}
