using System.Collections.Generic;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using HotpotLibrary.NotificationService;
using log4net;

public class RestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IEmailService _emailService; // Add this line
    private static readonly ILog _logger = LogManager.GetLogger(typeof(RestaurantService));

    public RestaurantService(IRestaurantRepository restaurantRepository, IEmailService emailService) // Add parameter
    {
        _restaurantRepository = restaurantRepository;
        _emailService = emailService; // Initialize the field
    }

    public IEnumerable<RestaurantDTO> GetAllRestaurants()
    {
        _logger.Info("Service: Fetching all restaurants.");
        return _restaurantRepository.GetAllRestaurants();
    }

    public RestaurantDTO GetRestaurantById(int restaurantId)
    {
        _logger.Info($"Service: Fetching restaurant with ID {restaurantId}.");
        return _restaurantRepository.GetRestaurantById(restaurantId);
    }

    public void CreateRestaurant(RestaurantDTO restaurantDto)
    {
        if (restaurantDto == null)
        {
            throw new ArgumentNullException(nameof(restaurantDto), "RestaurantDTO cannot be null.");
        }

        _logger.Info($"Service: Creating restaurant with Name: {restaurantDto.Name}");
        _restaurantRepository.CreateRestaurant(restaurantDto);

        // Send email notification
        var subject = "Welcome to HotPot!";
        var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {restaurantDto.Name},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Thank you for joining HotPot! Your restaurant has been successfully registered.
        </p>
        <p style='color: #777; text-align: center; margin-top: 40px;'>
            Best regards,<br/>
            HotPot Team
        </p>
        <p style='color: #777; text-align: center; margin-top: 40px;'>
            © 2024 HotPot. All rights reserved.
        </p>
    </div>
</body>
</html>";
        _emailService.SendEmail(restaurantDto.Email, subject, body);

        _logger.Info($"Service: Registration email sent to {restaurantDto.Email}");
    }

    public void UpdateRestaurant(int id, RestaurantDTO restaurantDto)
    {
        if (restaurantDto == null)
        {
            throw new ArgumentNullException(nameof(restaurantDto), "RestaurantDTO cannot be null.");
        }

        _logger.Info($"Service: Updating restaurant with ID {restaurantDto.RestaurantID}");
        var existingRestaurant = _restaurantRepository.GetRestaurantById(id);

        if (existingRestaurant == null)
        {
            _logger.Warn($"Service: Restaurant with ID {id} not found for update.");
            return;
        }

        _restaurantRepository.UpdateRestaurant(id, restaurantDto);

        // Send email notification
        var subject = "Your HotPot Restaurant Details Have Been Updated";
        var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {restaurantDto.Name},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Your restaurant details have been updated successfully. If you did not make this change, please contact support immediately.
        </p>
        <p style='color: #777; text-align: center; margin-top: 40px;'>
            Best regards,<br/>
            HotPot Team
        </p>
        <p style='color: #777; text-align: center; margin-top: 40px;'>
            © 2024 HotPot. All rights reserved.
        </p>
    </div>
</body>
</html>";
        _emailService.SendEmail(restaurantDto.Email, subject, body);

        _logger.Info($"Service: Update email sent to {restaurantDto.Email}");
    }

    public void DeleteRestaurant(int restaurantId)
    {
        _logger.Info($"Service: Deleting restaurant with ID {restaurantId}");
        var restaurant = _restaurantRepository.GetRestaurantById(restaurantId);

        if (restaurant == null)
        {
            _logger.Warn($"Service: Restaurant with ID {restaurantId} not found for deletion.");
            return;
        }

        _restaurantRepository.DeleteRestaurant(restaurantId);

        // Send email notification
        var subject = "Your HotPot Restaurant Has Been Deleted";
        var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {restaurant.Name},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Your restaurant has been successfully deleted from HotPot. If you believe this was a mistake, please contact support.
        </p>
        <p style='color: #777; text-align: center; margin-top: 40px;'>
            Best regards,<br/>
            HotPot Team
        </p>
        <p style='color: #777; text-align: center; margin-top: 40px;'>
            © 2024 HotPot. All rights reserved.
        </p>
    </div>
</body>
</html>";
        _emailService.SendEmail(restaurant.Email, subject, body);

        _logger.Info($"Service: Deletion email sent to {restaurant.Email}");
    }

    public RestaurantDTO GetRestaurantByNameAndLocation(string name, string location)
    {
        _logger.Info($"Service: Fetching restaurant with Name {name} and Location {location}.");
        return _restaurantRepository.GetRestaurantByNameAndLocation(name, location);
    }

    public int GetRestaurantIdByName(string restaurantName)
    {
        _logger.Info($"Service: Fetching restaurant ID with Name {restaurantName}.");
        return _restaurantRepository.GetRestaurantIdByName(restaurantName);
    }


    public RestaurantDTO GetRestaurantByEmail(string email)
    {
        _logger.Info($"Service: Fetching restaurant with Email {email}.");
        return _restaurantRepository.GetRestaurantByEmail(email);
    }

    public IEnumerable<RestaurantDTO> GetAllRestaurantsByCity(string city)
    {
        _logger.Info($"Service: Fetching all restaurants in City {city}.");
        return _restaurantRepository.GetAllRestaurantsByCity(city);
    }
}
