using System;
using System.Threading.Tasks;
using Payment.Api.Models;

namespace Payment.Api.Services
{
    public interface IPaymentService
    {
        Task<PaymentLinkPayByCreditCardResponseDTO> TakePayment(PaymentLinkPayByCreditCardRequestDTO requestModel);
    }

    public class PaymentService : IPaymentService
    {
        public async Task<PaymentLinkPayByCreditCardResponseDTO> TakePayment(PaymentLinkPayByCreditCardRequestDTO requestModel)
        {
            
            //Business Logic
            
            //Queue -> if purchase is valid.


            return await Task.Run(() => new PaymentLinkPayByCreditCardResponseDTO
            {
                ReceiptId = Guid.NewGuid().ToString()
            });
        }
    }
}