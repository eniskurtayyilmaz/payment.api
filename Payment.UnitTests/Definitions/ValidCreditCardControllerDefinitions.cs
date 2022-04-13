using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Payment.Api.Controllers;
using Payment.Api.Models;
using Payment.Api.Services;
using TechTalk.SpecFlow;

namespace Payment.UnitTests.Definitions
{
    [Binding]
    public class ValidCreditCardControllerDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private string _cvc;
        private string _cardowner;
        private string _exp;
        private string _cardnumber;

        private readonly Mock<IPaymentService> _paymentService;
        private readonly Fixture _fixture = new();
        public ValidCreditCardControllerDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _paymentService = new Mock<IPaymentService>();
        }


        [Given(@"the valid credit number is (.*)")]
        public void GivenTheValidCreditNumberIs(string cardnumber)
        {
            _cardnumber = cardnumber;
            _scenarioContext["cardnumber"] = _cardnumber;
        }

        [Given(@"the valid card owner (.*)")]
        public void GivenTheValidCardOwner(string cardowner)
        {
            _cardowner = cardowner;
        }


        [Given(@"the valid expiration date (.*)")]
        public void GivenTheValidExpirationDate(string exp)
        {
            _exp = exp;
        }

        [Given(@"the valid CVC (.*)")]
        public void GivenTheValidCVC(string cvc)
        {
            _cvc = cvc;
        }


        [When(@"I call the API /api/paymentLink with credit card number, card owner, expiration date and cvc")]
        public void WhenISetValidationsGettingValidators()
        {

            _paymentService.Setup(x => x.TakePayment(It.IsAny<PaymentLinkPayByCreditCardRequestDto>()))
                .Returns(_fixture.Create<PaymentLinkPayByCreditCardResponseDto>());

            var controller = new PaymentLinkController(_paymentService.Object);

            var model = new PaymentLinkPayByCreditCardRequestDto
            {
                CardOwner = _cardowner,
                CreditCardNumber = _cardnumber,
                IssueDate = _exp,
                CVC = _cvc
            };

            var response = controller.PayByCreditCard(model);

            var okResponse = response as OkObjectResult;

            var responseObj = okResponse.Value as PaymentLinkPayByCreditCardResponseDto;
            _scenarioContext["object"] = responseObj;
            _scenarioContext["responseCode"] = (HttpStatusCode)okResponse.StatusCode;

            _paymentService.Verify(x=> x.TakePayment(It.IsAny<PaymentLinkPayByCreditCardRequestDto>()), Times.Once());
        }


        [Then(@"I see receipt ID")]
        public void ThenISeeReceiptId()
        {
            var responseObj = _scenarioContext["object"] as PaymentLinkPayByCreditCardResponseDto;
            responseObj.Should().NotBeNull();
            responseObj.ReceiptId.Should().NotBeNull();
            responseObj.ReceiptId.Should().NotBeEmpty();
        }

        [Then(@"I see response status code is OK")]
        public void ThenResponseStatusCodeOK()
        {
            var responseCode = (HttpStatusCode)_scenarioContext["responseCode"];
            responseCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
