using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Payment.Api.Constants;
using Payment.Api.Models;
using Payment.Api.Resources;
using Payment.Api.Validators;
using Payment.IntegrationTests.Common;
using Payment.IntegrationTests.Models;
using TechTalk.SpecFlow;

namespace Payment.IntegrationTests.Definitions
{
    [Binding]
    public class CreditCardNumberDefinitions : TestInitialize
    {
        private readonly ScenarioContext _scenarioContext;
        private string _cardnumber;

        public CreditCardNumberDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the credit card number is (.*) from Examples")]
        public void GivenTheCreditCardNumberIs(string cardnumber)
        {
            _cardnumber = cardnumber;
        }

        [Given(@"the credit card number is empty")]
        public void GivenTheCreditCardNumberIsEmpty()
        {
            _cardnumber = string.Empty;
        }

        [Given(@"the credit card number is null")]
        public void GivenTheCreditCardNumberIsNull()
        {
            _cardnumber = null;
        }

        [When(@"I call the API /api/paymentLink")]
        public async Task WhenISetValidationsGettingValidators()
        {

            var response = await this.Client.PostAsync(Constant.PaymentLinkEndpoint, JsonData(
                new PaymentLinkPayByCreditCardRequestDTO
                {
                    CreditCardNumber = _cardnumber
                }));

            var responseObj = await response.Content.ReadFromJsonAsync<ValidateErrorResult>();
            _scenarioContext["object"] = responseObj;
            _scenarioContext["responseCode"] = response.StatusCode;
        }
        
        [Then(@"I see in response that Card number must be numeric with 15-16 length")]
        public void ThenCreditCardNumberMustNumeric()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.CreditCard)
                .Errors
                .Any(x => x == ErrorMessagesResources.CardNumberMustBeNumericWith15_16Length).Should().BeTrue();

        }

        [Then(@"I see in response that Card number can not be null or empty")]
        public void ThenCreditCardNumberCanNotBeNullOrEmpty()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.CreditCard)
                .Errors
                .Any(x => x == ErrorMessagesResources.CardNumberCanNotBeNullOrEmpty).Should().BeTrue();
        }

        [Then(@"I see response status code is BadRequest")]
        public void ThenISeeResponseStatusCodeIsBadRequest()
        {
            var responseCode = (HttpStatusCode)_scenarioContext["responseCode"];
            responseCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
