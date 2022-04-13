using FluentAssertions;
using Payment.Api.Models;
using Payment.Api.Services;
using Xunit;

namespace Payment.UnitTests.Services
{
    public class PaymentServiceTests
    {
        public PaymentServiceTests()
        {

        }

        [Fact]

        public void When_Call_PaymentService_PaymentCreditCard()
        {
            var paymentService = new PaymentService();

            var result = paymentService.TakePayment(new PaymentLinkPayByCreditCardRequestDto());

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<PaymentLinkPayByCreditCardResponseDto>();
            result.ReceiptId.Should().NotBeNullOrEmpty();
        }
    }
}