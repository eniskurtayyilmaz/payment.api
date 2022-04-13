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
    public class UnknownCreditCardDefinitions : TestInitialize
    {
        private readonly ScenarioContext _scenarioContext;
        private string _unknownCreditCard;

        public UnknownCreditCardDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the unknown credit card is (.*) from Examples")]
        public void GivenTheUnknownCreditCardNumberIs(string cardnumber)
        {
            this._unknownCreditCard = cardnumber;
        }

        [When(@"I call the API /api/paymentLink with unknown credit card")]
        public async Task WhenISetValidationsGettingValidators()
        {

            var response = await this.Client.PostAsync(Constant.PaymentLinkEndpoint, JsonData(
                new PaymentLinkPayByCreditCardRequestDto()
                {
                    CreditCardNumber = _unknownCreditCard
                }));

            var responseObj = await response.Content.ReadFromJsonAsync<ValidateErrorResult>();
            _scenarioContext["object"] = responseObj;
            _scenarioContext["responseCode"] = response.StatusCode;
        }

        [Then(@"I see in response that Only American Express, Visa, or Mastercard cards accepted")]
        public void ThenUnknownCreditCardNumber()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.CreditCard)
                .Errors
                .Any(x => x == ErrorMessagesResources.CreditCardOnlyAcceptedCards).Should().BeTrue();

        }
    }
}
