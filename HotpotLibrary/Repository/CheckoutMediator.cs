using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using System;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class CheckoutMediator : ICheckoutMediator
    {
        private readonly ICartInterface _cartRepository;
        private readonly IOrderInterface _orderRepository;
        private readonly IOrderDetailInterface _orderDetailsRepository;
        private readonly IPaymentInterface _paymentRepository;

        public CheckoutMediator(
            ICartInterface cartRepository,
            IOrderInterface orderRepository,
            IOrderDetailInterface orderDetailsRepository,
            IPaymentInterface paymentRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _paymentRepository = paymentRepository;
        }

        public void AddToCart(CartDTO cartDTO)
        {
            // Create a cart item directly from CartDTO
            _cartRepository.CreateCart(cartDTO);
        }

        public void PlaceOrder(OrderDTO orderDTO)
        {
            // Create the order and allow the database to generate the OrderID
            _orderRepository.CreateOrder(orderDTO);

            // Check if OrderID is set after creation
            if (orderDTO.OrderID <= 0) // Ensure OrderID is generated and valid
            {
                throw new InvalidOperationException("Order creation failed; OrderID is not set.");
            }

            // Get cart items for the user
            var cartItems = _cartRepository.GetCartItemsByUserId(orderDTO.UserID);

            // Check if there are any cart items
            if (cartItems == null || !cartItems.Any())
            {
                throw new InvalidOperationException("No items found in the cart.");
            }

            // Iterate over each cart item to create order details
            foreach (var item in cartItems)
            {
                // Create OrderDetailDTO from cart item
                var orderDetailDTO = new OrderDetailDTO
                {
                    OrderID = orderDTO.OrderID, // Use the generated OrderID
                    MenuItemID = item.MenuItemID,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Subtotal = item.Quantity * item.Price
                };

                // Create order details using OrderDetailDTO
                _orderDetailsRepository.CreateOrderDetails(orderDetailDTO);
            }
            _cartRepository.DeleteCartByUserId(orderDTO.UserID);
        }




        public void ProcessPayment(PaymentDTO paymentDTO)
        {
            // Fetch the order to get the total amount
            var orderDTO = _orderRepository.GetOrderById(paymentDTO.OrderID);
            if (orderDTO == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            // Create the payment entry
            var payment = new PaymentDTO
            {
                OrderID = paymentDTO.OrderID,
                PaymentMethod = paymentDTO.PaymentMethod,
                AmountPaid = orderDTO.TotalAmount, // Set amount paid from the order total
                TransactionDate = paymentDTO.TransactionDate,
                TransactionStatus = paymentDTO.TransactionStatus
            };
            _paymentRepository.CreatePayment(payment);

            // Update order payment status
            orderDTO.PaymentStatus = paymentDTO.TransactionStatus; // Assuming TransactionStatus maps to PaymentStatus
            _orderRepository.UpdateOrder(orderDTO.OrderID, orderDTO);
        }
    }
}
