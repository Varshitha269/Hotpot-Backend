using HotpotLibrary.DTO;
using HotpotLibrary.Interfaces;

namespace HotpotLibrary.Services
{
    public class CheckoutService
    {
        private readonly ICheckoutMediator _mediator;

        public CheckoutService(ICheckoutMediator mediator)
        {
            _mediator = mediator;
        }

        public void AddItemToCart(CartDTO cartDTO)
        {
            _mediator.AddToCart(cartDTO);
        }

        public void PlaceOrder(OrderDTO orderDTO)
        {
            _mediator.PlaceOrder(orderDTO);
        }

        public void ProcessPayment(PaymentDTO paymentDTO)
        {
            _mediator.ProcessPayment(paymentDTO);
        }
    }
}
