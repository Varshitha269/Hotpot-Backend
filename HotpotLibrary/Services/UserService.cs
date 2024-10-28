using System.Collections.Generic;
using System.Linq;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using HotpotLibrary.NotificationService;
using log4net;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService; // Add this line
    private static readonly ILog _logger = LogManager.GetLogger(typeof(UserService));

    public UserService(IUserRepository userRepository, IEmailService emailService) // Add parameter
    {
        _userRepository = userRepository;
        _emailService = emailService; // Initialize the field
    }

    public IEnumerable<UserDTO> GetAllUsers()
    {
        _logger.Info("Service: Fetching all users.");
        return _userRepository.GetAllUsers();
    }

    public UserDTO GetUserById(int userId)
    {
        _logger.Info($"Service: Fetching user with ID {userId}.");
        return _userRepository.GetUserById(userId);
    }

    public void CreateUser(UserDTO userDto)
    {
        _logger.Info($"Service: Creating user with Username: {userDto.Username}");

        // Create the user
        _userRepository.CreateUser(userDto);

        // Send email notification
        var subject = "Welcome to HotPot!";
        var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Welcome to HotPot, {userDto.Username}!</h1>
            <p style='color: #777;'>You received this email because you registered on HotPot.</p>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Thank you for registering with HotPot. Your registration is successful.
        </p>
        <p style='color: #777; text-align: center; margin-top: 40px;'>
            © 2024 HotPot. All rights reserved.
        </p>
    </div>
</body>
</html>";
        _emailService.SendEmail(userDto.Email, subject, body);

        _logger.Info($"Service: Registration email sent to {userDto.Email}");
    }

    public void UpdateUser(int id, UserDTO userDto)
    {
        _logger.Info($"Service: Updating user with ID {userDto.UserID}");
        var existingUser = _userRepository.GetUserById(id);

        if (existingUser == null)
        {
            _logger.Warn($"Service: User with ID {id} not found for update.");
            return;
        }

        _userRepository.UpdateUser(id, userDto);

        // Send email notification
        var subject = "Your HotPot Account Has Been Updated";
        var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {userDto.Username},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Your account details have been updated successfully. If you did not make this change, please contact support immediately.
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
        _emailService.SendEmail(userDto.Email, subject, body);

        _logger.Info($"Service: Update email sent to {userDto.Email}");
    }

    public void DeleteUser(int userId)
    {
        _logger.Info($"Service: Deleting user with ID {userId}");
        var user = _userRepository.GetUserById(userId);

        if (user == null)
        {
            _logger.Warn($"Service: User with ID {userId} not found for deletion.");
            return;
        }

        _userRepository.DeleteUser(userId);

        // Send email notification
        var subject = "Your HotPot Account Has Been Deleted";
        var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {user.Username},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Your account has been successfully deleted. If you believe this was a mistake, please contact support.
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
        _emailService.SendEmail(user.Email, subject, body);

        _logger.Info($"Service: Deletion email sent to {user.Email}");
    }

    public UserDTO GetUserByUsername(string username)
    {
        _logger.Info($"Service: Fetching user with Username: {username}");
        var user = _userRepository.GetUserByUsername(username);
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
            CreatedDate = user.CreatedDate,
            IsActive = user.IsActive
        };
    }

    public UserDTO GetUserByEmail(string email)
    {
        _logger.Info($"Service: Fetching user with Email: {email}");
        var user = _userRepository.GetUserByEmail(email);
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
            CreatedDate = user.CreatedDate,
            IsActive = user.IsActive
        };
    }

    public IEnumerable<FeedbackRatingDTO> GetUserReviews(int userId)
    {
        _logger.Info($"Service: Fetching reviews for user with ID {userId}");
        var reviews = _userRepository.GetUserReviews(userId);
        return reviews.Select(fr => new FeedbackRatingDTO
        {
            FeedbackRatingID = fr.FeedbackRatingID,
            UserID = fr.UserID,
            RestaurantID = fr.RestaurantID,
            Message = fr.Message,
            Rating = fr.Rating,
            CreatedDate = fr.CreatedDate
        }).ToList();
    }

    public IEnumerable<OrderDTO> GetAllOrdersByUserId(int userId)
    {
        _logger.Info($"Service: Fetching all orders for user with ID {userId}");
        var orders = _userRepository.GetAllOrdersByUserId(userId);
        return orders.Select(o => new OrderDTO
        {
            OrderID = o.OrderID,
            UserID = o.UserID,
            RestaurantID = o.RestaurantID,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            OrderStatus = o.OrderStatus,
            PaymentStatus = o.PaymentStatus,
            DeliveryAddress = o.DeliveryAddress,
            DeliveryDate = o.DeliveryDate,
            CreatedDate = o.CreatedDate
        }).ToList();
    }
}
