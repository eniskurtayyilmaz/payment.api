using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment.Api.Models;
using Payment.Api.Services;

namespace Payment.Api.Controllers
{
    [ApiController]
    [Route("api/paymentLink")]
    public class PaymentLinkController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentLinkController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        
        [HttpPost]
        public async Task<IActionResult> PayByCreditCard([FromBody] PaymentLinkPayByCreditCardRequestDTO model)
        {
            return Ok(await _paymentService.TakePayment(model));
        }
    }
}