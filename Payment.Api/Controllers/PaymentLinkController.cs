using System;
using System.Collections.Generic;
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

            var validatorFactory = new ValidatorHandler(model);
            validatorFactory.SetValidators(new List<IValidator>()
            {
                new CardOwnerInformationValidator(model.CardOwner),
                new CvcValidator(model.CVC),
                new ExpireDateValidator(model.IssueDate),
                new CardNumberValidator(model.CreditCardNumber),
                new CreditCardTypeFactoryBuilder(model.CreditCardNumber).SetDefaultValidators()
            });

            var resultOfValidation = validatorFactory.Validate();
            if (!resultOfValidation.IsValid)
            {
                return BadRequest(new { Error = resultOfValidation.Error });
            }


            return Ok(await _paymentService.TakePayment(model));
        }
    }
}