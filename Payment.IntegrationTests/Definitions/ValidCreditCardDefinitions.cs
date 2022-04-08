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
    public class ValidCreditCardDefinitions : TestInitialize
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorHandler _validatorFactory;
        private string _cvc;
        private string _cardowner;
        private string _exp;
        private string _cardnumber;


        public ValidCreditCardDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the valid credit number is (.*)")]
        public void GivenTheValidCreditNumberIs(string cardnumber)
        {
            _cardnumber = cardnumber;
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
        public async Task WhenISetValidationsGettingValidators()
        {

            var response = await this.Client.PostAsync(Constant.PaymentLinkEndpoint, JsonData(
                new PaymentLinkPayByCreditCardRequestDTO
                {
                    CardOwner = _cardowner,
                    CreditCardNumber = _cardnumber,
                    IssueDate = _exp,
                    CVC = _cvc
                }));

            var responseObj = await response.Content.ReadFromJsonAsync<PaymentLinkPayByCreditCardResponseDTO>();
            _scenarioContext["object"] = responseObj;
            _scenarioContext["responseCode"] = response.StatusCode;
        }


        [Then(@"I see receipt ID")]
        public void ThenISeeReceiptId()
        {
            var responseObj = _scenarioContext["object"] as PaymentLinkPayByCreditCardResponseDTO;
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
