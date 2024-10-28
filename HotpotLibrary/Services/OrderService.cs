using System;
using System.Collections.Generic;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using HotpotLibrary.NotificationService;
using log4net;

namespace HotpotLibrary.Services
{
    public class OrderService
    {
        private readonly IOrderInterface _orderRepository;
        private readonly IUserRepository _userRepository; // To retrieve user details
        private readonly IEmailService _emailService; // For sending emails
        private static readonly ILog _logger = LogManager.GetLogger(typeof(OrderService));

        public OrderService(IOrderInterface orderRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        // Get Order by ID
        public OrderDTO GetOrderById(int id)
        {
            _logger.Info($"Fetching order with ID {id}.");
            return _orderRepository.GetOrderById(id);
        }

        public List<OrderDTO> GetOrdersByUserId(int userId)
        {
            _logger.Info($"Fetching orders for user with ID {userId}.");
            return _orderRepository.GetOrdersByUserId(userId);
        }

        public List<OrderDTO> GetOrdersByRestaurantId(int restaurantId)
        {
            _logger.Info($"Fetching orders for restaurant with ID {restaurantId}.");
            return _orderRepository.GetOrdersByRestaurantId(restaurantId);
        }
        public void UpdateOrderStatus(int orderId, string status)
        {
            _logger.Info($"Updating status of order with ID {orderId} to {status}.");

            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                _logger.Warn($"Order with ID {orderId} not found.");
                return;
            }

            // Update the order status
            _orderRepository.UpdateOrderStatus(orderId, status);

            // Fetch the user's email using the UserID in the order
            var user = _userRepository.GetUserById(order.UserID);
            if (user == null)
            {
                _logger.Warn($"User with ID {order.UserID} not found. Cannot send order status update email.");
                return;
            }

            // Send email notification to the user
            var subject = "Order Status Update";
            var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {user.Username},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Your order with ID <strong>{orderId}</strong> has been updated to: <strong>{status}</strong>.
        </p>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Thank you for ordering from HotPot!
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

            _logger.Info($"Order status update email sent to {user.Email}");
        }


        // Get all orders
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            _logger.Info("Fetching all orders.");
            return _orderRepository.GetAllOrders();
        }

        // Create a new order
        public void CreateOrder(OrderDTO orderDto)
        {
            if (orderDto == null)
            {
                throw new ArgumentNullException(nameof(orderDto), "OrderDTO cannot be null.");
            }

            _logger.Info($"Creating order with ID {orderDto.OrderID}.");
            _orderRepository.CreateOrder(orderDto);

            // Fetch the user's email using the UserID in the order
            var user = _userRepository.GetUserById(orderDto.UserID);
            if (user == null)
            {
                _logger.Warn($"User with ID {orderDto.UserID} not found. Cannot send order confirmation email.");
                return;
            }

            // Send email notification to the user
            var subject = "Order Confirmation";
            var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {user.Username},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Your order with ID <strong>{orderDto.OrderID}</strong> has been successfully placed!
        </p>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Total Amount: <strong>{orderDto.TotalAmount}</strong>
        </p>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Thank you for ordering from HotPot!
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

            _logger.Info($"Order confirmation email sent to {user.Email}");
        }

        // Update an order
        public void UpdateOrder(int id, OrderDTO orderDto)
        {
            if (orderDto == null)
            {
                throw new ArgumentNullException(nameof(orderDto), "OrderDTO cannot be null.");
            }

            _logger.Info($"Updating order with ID {id}.");

            // Fetch the existing order to get the user ID for email notification
            var existingOrder = _orderRepository.GetOrderById(id);
            if (existingOrder == null)
            {
                _logger.Warn($"Order with ID {id} not found. Update operation cannot be performed.");
                return;
            }

            // Update the order
            _orderRepository.UpdateOrder(id, orderDto);

            // Fetch the user's email using the UserID in the order
            var user = _userRepository.GetUserById(existingOrder.UserID);
            if (user == null)
            {
                _logger.Warn($"User with ID {existingOrder.UserID} not found. Cannot send order update email.");
                return;
            }

            // Send email notification to the user
            var subject = "Your HotPot Order Has Been Updated";
            var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {user.Username},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Your order with ID <strong>{id}</strong> has been successfully updated.
        </p>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            If you have any questions, please contact us.
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
            _logger.Info($"Order update email sent to {user.Email}");
        }


        // Delete an order
        public void DeleteOrder(int id)
        {
            _logger.Info($"Deleting order with ID {id}.");
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                _logger.Warn($"Order with ID {id} not found.");
                return;
            }

            // Fetch the user's email using the UserID in the order
            var user = _userRepository.GetUserById(order.UserID);
            if (user == null)
            {
                _logger.Warn($"User with ID {order.UserID} not found. Cannot send order cancellation email.");
                return;
            }

            _orderRepository.DeleteOrder(id);

            // Send email notification to the user
            var subject = "Order Cancellation";
            var body = $@"
<html>
<body style='font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 0; margin: 0;'>
    <div style='max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);'>
        <div style='text-align: center;'>
            <h1 style='color: #333; margin-bottom: 0;'>Hello {user.Username},</h1>
        </div>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            Your order with ID <strong>{id}</strong> has been successfully canceled.
        </p>
        <p style='color: #555; text-align: center; font-size: 16px;'>
            If you have any questions, please contact us.
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

            _logger.Info($"Order cancellation email sent to {user.Email}");
        }
    


       
    }
}
