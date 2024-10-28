using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using log4net;

namespace HotpotLibrary.Services
{
    public class PaymentsService
    {
        private readonly IPaymentInterface _paymentsRepository;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PaymentsService));

        public PaymentsService(IPaymentInterface paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public PaymentDTO GetPaymentById(int id)
        {
            _logger.Info($"Fetching payment with ID {id}.");
            return _paymentsRepository.GetPaymentById(id);
        }

        public IEnumerable<PaymentDTO> GetAllPayments()
        {
            _logger.Info("Fetching all payments.");
            return _paymentsRepository.GetAllPayments();
        }

        public void CreatePayment(PaymentDTO paymentDto)
        {
            if (paymentDto == null)
            {
                throw new ArgumentNullException(nameof(paymentDto), "PaymentDTO cannot be null.");
            }
            _logger.Info($"Creating payment with ID {paymentDto.PaymentID}.");
            _paymentsRepository.CreatePayment(paymentDto);
        }

        public void UpdatePayment(int id, PaymentDTO paymentDto)
        {
            if (paymentDto == null)
            {
                throw new ArgumentNullException(nameof(paymentDto), "PaymentDTO cannot be null.");
            }
            _logger.Info($"Updating payment with ID {id}.");
            _paymentsRepository.UpdatePayment(id, paymentDto);
        }

        public void DeletePayment(int id)
        {
            _logger.Info($"Deleting payment with ID {id}.");
            _paymentsRepository.DeletePayment(id);
        }

        public void CreatePayment(Payment paymentDto)
        {
            throw new NotImplementedException("This method is not implemented.");
        }
    }
}
