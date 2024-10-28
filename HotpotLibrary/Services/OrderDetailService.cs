using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using log4net;

namespace HotpotLibrary.Services
{
    public class OrderDetailsService
    {
        private readonly IOrderDetailInterface _orderDetailsRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(OrderDetailsService));

        public OrderDetailsService(IOrderDetailInterface orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public OrderDetailDTO GetOrderDetailsById(int id)
        {
            _logger.Info($"Fetching order details with ID {id}.");
            return _orderDetailsRepository.GetOrderDetailsById(id);
        }

        public IEnumerable<OrderDetailDTO> GetAllOrderDetails()
        {
            _logger.Info("Fetching all order details.");
            return _orderDetailsRepository.GetAllOrderDetails();
        }

        public void CreateOrderDetails(OrderDetailDTO orderDetailDto)
        {
            if (orderDetailDto == null)
            {
                throw new ArgumentNullException(nameof(orderDetailDto), "OrderDetailDTO cannot be null.");
            }
            _logger.Info($"Creating order details with ID {orderDetailDto.OrderDetailID}.");
            _orderDetailsRepository.CreateOrderDetails(orderDetailDto);
        }

        public void UpdateOrderDetails(int id, OrderDetailDTO orderDetailDto)
        {
            if (orderDetailDto == null)
            {
                throw new ArgumentNullException(nameof(orderDetailDto), "OrderDetailDTO cannot be null.");
            }
            _logger.Info($"Updating order details with ID {id}.");
            _orderDetailsRepository.UpdateOrderDetails(id, orderDetailDto);
        }

        public void DeleteOrderDetails(int id)
        {
            _logger.Info($"Deleting order details with ID {id}.");
            _orderDetailsRepository.DeleteOrderDetails(id);
        }
    }
}
