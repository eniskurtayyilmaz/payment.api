using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Payment.Api.Constants;
using Payment.Api.Models;
using Payment.Api.Resources;
using Payment.Api.Validators;
using Payment.IntegrationTests.Common;
using TechTalk.SpecFlow;

namespace Payment.IntegrationTests.Definitions
{
    [Binding]
    public class ExpirationDateDefinitions : TestInitialize
    {
        private readonly ScenarioContext _scenarioContext;
        private string _exp;

        public ExpirationDateDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the expiration date is (.*) from Examples")]
        public void GivenTheExpirationDateNumberIs(string exp)
        {
            _exp = exp;
        }

        [Given(@"the expiration date is empty")]
        public void GivenTheExpirationDateNumberIsEmpty()
        {
            _exp = string.Empty;
        }

        [Given(@"the expiration date is null")]
        public void GivenTheExpirationDateNumberIsNull()
        {
            _exp = null;
        }

        [When(@"I call the API /api/paymentLink with expiration date")]
        public async Task WhenISetValidationsGettingValidators()
        {

            var response = await this.Client.PostAsync(Constant.PaymentLinkEndpoint, JsonData(
                new PaymentLinkPayByCreditCardRequestDto()
                {
                    IssueDate = _exp
                }));

            var responseObj = await response.Content.ReadFromJsonAsync<ValidateErrorResult>();
            _scenarioContext["object"] = responseObj;
            _scenarioContext["responseCode"] = response.StatusCode;
        }

        [Then(@"I see in response that expiration date must be MM/YY format")]
        public void ThenExpirationDateNumberMustBeMMYYFormat()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.ExpirationDate)
                .Errors
                .Any(x => x == ErrorMessagesResources.ExpirationDateMustBeMMYYFormat).Should().BeTrue();

        }

        [Then(@"I see in response that expiration date can not be null or empty")]
        public void ThenExpirationDateNumberCanNotBeNullOrEmpty()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.ExpirationDate)
                .Errors
                .Any(x => x == ErrorMessagesResources.ExpirationDateCanNotBeNullOrEmpty).Should().BeTrue();
        }

        [Given(@"the expired expiration date is (.*)")]
        public void GivenTheExpiredExpirationDateIs(string exp)
        {
            _exp = exp;
        }

        [When(@"I call the API /api/paymentLink with credit card number, expiration date")]
        public async Task WhenICallTheAPIApiPaymentLinkWithCreditCardNumberExpirationDate()
        {

            var response = await this.Client.PostAsync(Constant.PaymentLinkEndpoint, JsonData(
                new PaymentLinkPayByCreditCardRequestDto()
                {
                    IssueDate = _exp,
                    CreditCardNumber = _scenarioContext["cardnumber"] as string
                }));

            var responseObj = await response.Content.ReadFromJsonAsync<ValidateErrorResult>();
            _scenarioContext["object"] = responseObj;
            _scenarioContext["responseCode"] = response.StatusCode;
        }


        [Then(@"I see in response that the credit card has been expired")]
        public void ThenISeeInResponseThatTheCreditCardHasBeenExpired()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.ExpirationDate)
                .Errors
                .Any(x => x == ErrorMessagesResources.CreditCardExpirationDateExpired).Should().BeTrue();
        }


    }
}
