using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payment.Api.Models;
using Payment.Api.Validators;

namespace Payment.Api.Services
{
    public interface IPaymentService
    {
        PaymentLinkPayByCreditCardResponseDto TakePayment(PaymentLinkPayByCreditCardRequestDto requestModel);
    }

    public class PaymentService : IPaymentService
    {
        public PaymentLinkPayByCreditCardResponseDto TakePayment(PaymentLinkPayByCreditCardRequestDto requestModel)
        {
            return new PaymentLinkPayByCreditCardResponseDto
            {
                ReceiptId = Guid.NewGuid().ToString()
            };
        }
    }
}