using System.Collections.Generic;
using HotpotLibrary.DTO;
using HotpotLibrary.Models;

public interface IRestaurantRepository
{
    IEnumerable<RestaurantDTO> GetAllRestaurants();
    RestaurantDTO GetRestaurantById(int restaurantId);
    void CreateRestaurant(RestaurantDTO restaurantDto);
    void UpdateRestaurant(int id, RestaurantDTO restaurantDto);
    void DeleteRestaurant(int restaurantId);
    public int GetRestaurantIdByName(string restaurantName);

    RestaurantDTO GetRestaurantByNameAndLocation(string name, string location);
    RestaurantDTO GetRestaurantByEmail(string email);
    IEnumerable<RestaurantDTO> GetAllRestaurantsByCity(string city);

}
