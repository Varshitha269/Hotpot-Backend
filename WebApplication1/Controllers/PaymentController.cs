using HotpotLibrary.DTO;
using HotpotLibrary.Models;
using HotpotLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsService _paymentsService;

        public PaymentsController(PaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var payment = _paymentsService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetAllPayments()
        {
            return Ok(_paymentsService.GetAllPayments());
        }

        [HttpPost]
        public IActionResult CreatePayment([FromBody] PaymentDTO paymentDto)
        {
            _paymentsService.CreatePayment(paymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = paymentDto.PaymentID }, paymentDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] PaymentDTO paymentDto)
        {
            if (id != paymentDto.PaymentID)
            {
                return BadRequest();
            }
            _paymentsService.UpdatePayment(id, paymentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            _paymentsService.DeletePayment(id);
            return NoContent();
        }
    }
}
