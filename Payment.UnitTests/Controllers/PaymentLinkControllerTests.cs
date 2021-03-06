using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Payment.Api.Controllers;
using Payment.Api.Models;
using Payment.Api.Services;
using Xunit;

namespace Payment.UnitTests.Controllers
{

    public class PaymentLinkControllerTests
    {
        private Mock<IPaymentService> _paymentService = new Mock<IPaymentService>();
        private Fixture _fixture = new();
        public PaymentLinkControllerTests()
        {

        }

        [Fact]

        public void Given_Handler_Has_Error_Then_Result_Must_Be_Bad_Request()
        {
            var controller = new PaymentLinkController(_paymentService.Object);
            var model = _fixture.Create<PaymentLinkPayByCreditCardRequestDto>();

            var result = controller.PayByCreditCard(model);

            var badRequest = result as BadRequestObjectResult;

            badRequest.Should().NotBeNull();
            badRequest.StatusCode.Should().Be(400);
            badRequest.Value.Should().BeAssignableTo<ValidateErrorResult>();

            _paymentService.Verify(x => x.TakePayment(It.IsAny<PaymentLinkPayByCreditCardRequestDto>()), Times.Never);
        }

        [Fact]

        public void Passed_Validator_Then_Result_Must_Be_Ok()
        {
            _paymentService.Setup(x => x.TakePayment(It.IsAny<PaymentLinkPayByCreditCardRequestDto>()))
                .Returns(_fixture.Create<PaymentLinkPayByCreditCardResponseDto>());

            var controller = new PaymentLinkController(_paymentService.Object);
            var model = new PaymentLinkPayByCreditCardRequestDto
            {
                CardOwner = "Enis Kurtay",
                CreditCardNumber = "4012888888881881",
                IssueDate = "08/29",
                CVC = "555"
            };

            var result = controller.PayByCreditCard(model);

            var badRequest = result as OkObjectResult;

            badRequest.Should().NotBeNull();
            badRequest.StatusCode.Should().Be(200);
            badRequest.Value.Should().BeAssignableTo<PaymentLinkPayByCreditCardResponseDto>();

            var value = badRequest.Value as PaymentLinkPayByCreditCardResponseDto;
            value.ReceiptId.Should().NotBeNullOrEmpty();

            _paymentService.Verify(x => x.TakePayment(It.IsAny<PaymentLinkPayByCreditCardRequestDto>()), Times.Once);
        }
    }
}
