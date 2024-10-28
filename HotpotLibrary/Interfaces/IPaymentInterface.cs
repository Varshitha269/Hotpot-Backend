using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface IPaymentInterface
    {
        PaymentDTO GetPaymentById(int id);
        IEnumerable<PaymentDTO> GetAllPayments();
        void CreatePayment(PaymentDTO paymentDto);
        void UpdatePayment(int id, PaymentDTO paymentDto);
        void DeletePayment(int id);
    }
}
