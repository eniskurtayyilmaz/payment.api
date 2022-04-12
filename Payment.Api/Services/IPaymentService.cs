using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payment.Api.Models;
using Payment.Api.Validators;

namespace Payment.Api.Services
{
    public interface IPaymentService
    {
        Task<PaymentLinkPayByCreditCardResponseDto> TakePayment(PaymentLinkPayByCreditCardRequestDto requestModel);
    }

    public class PaymentService : IPaymentService
    {
        public async Task<PaymentLinkPayByCreditCardResponseDto> TakePayment(PaymentLinkPayByCreditCardRequestDto requestModel)
        {

            //Business Logic
          
            //Queue -> if purchase is valid.


            return await Task.Run(() => new PaymentLinkPayByCreditCardResponseDto
            {
                ReceiptId = Guid.NewGuid().ToString()
            });
        }
    }
}