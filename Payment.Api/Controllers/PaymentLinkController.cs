using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment.Api.Models;
using Payment.Api.Services;
using Payment.Api.Validators;

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
            var validatorHandler = new ValidatorHandler(model);

            var resultOfErrorValidatorResults = validatorHandler.Validate();
            if (resultOfErrorValidatorResults != null)
            {
                return BadRequest(resultOfErrorValidatorResults);
            }

            return Ok(await _paymentService.TakePayment(model));
        }
    }
}