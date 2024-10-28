using HotpotLibrary.Data;
using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;
using HotpotLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotpotLibrary.Repository
{
    public class PaymentRepository : IPaymentInterface
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public PaymentDTO GetPaymentById(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null) return null;

            return new PaymentDTO
            {
                PaymentID= payment.PaymentID,
                OrderID = payment.OrderID,
                PaymentMethod = payment.PaymentMethod,
                AmountPaid = payment.AmountPaid,
                TransactionDate = (DateTime)payment.TransactionDate,
                TransactionStatus = payment.TransactionStatus
            };
        }

        public IEnumerable<PaymentDTO> GetAllPayments()
        {
            return _context.Payments.Select(payment => new PaymentDTO
            {
                PaymentID = payment.PaymentID,
                OrderID = payment.OrderID,
                PaymentMethod = payment.PaymentMethod,
                AmountPaid = payment.AmountPaid,
                TransactionDate = (DateTime)payment.TransactionDate,
                TransactionStatus = payment.TransactionStatus
            }).ToList();
        }

        public void CreatePayment(PaymentDTO paymentDto)
        {
            var payment = new Payment
            {
                OrderID = paymentDto.OrderID,
                PaymentMethod = paymentDto.PaymentMethod,
                AmountPaid = paymentDto.AmountPaid,
                TransactionDate = paymentDto.TransactionDate,
                TransactionStatus = paymentDto.TransactionStatus
            };
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void UpdatePayment(int id, PaymentDTO paymentDto)
        {
            var payment = _context.Payments.Find(id);
            if (payment != null)
            {
                payment.OrderID = paymentDto.OrderID;
                payment.PaymentMethod = paymentDto.PaymentMethod;
                payment.AmountPaid = paymentDto.AmountPaid;
                payment.TransactionDate = paymentDto.TransactionDate;
                payment.TransactionStatus = paymentDto.TransactionStatus;

                _context.Payments.Update(payment);
                _context.SaveChanges();
            }
        }

        public void DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }
    }
}
