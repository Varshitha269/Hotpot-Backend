using HotpotLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.Interfaces
{
    public interface ICheckoutMediator
    {
        void AddToCart(CartDTO cartDTO);
        void PlaceOrder(OrderDTO orderDTO);
        void ProcessPayment(PaymentDTO paymentDTO);
    }
}
