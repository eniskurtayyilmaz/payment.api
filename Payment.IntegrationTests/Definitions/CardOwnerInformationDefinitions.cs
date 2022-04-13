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
    public class CardOwnerInformationDefinitions : TestInitialize
    {
        private readonly ScenarioContext _scenarioContext;
        private string _cardOwnerInformation;

        public CardOwnerInformationDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the card owner information is (.*) from Examples")]
        public void GivenTheCardOwnerInformationIs(string cardowner)
        {
            this._cardOwnerInformation = cardowner;
        }

        [Given(@"the card owner information is empty")]
        public void GivenTheCardOwnerInformationIsEmpty()
        {
            _cardOwnerInformation = string.Empty;
        }

        [Given(@"the card owner information is null")]
        public void GivenTheCardOwnerInformationIsNull()
        {
            _cardOwnerInformation = null;
        }

        [When(@"I call the API /api/paymentLink with card owner information")]
        public async Task WhenISetValidationsGettingValidators()
        {

            var response = await this.Client.PostAsync(Constant.PaymentLinkEndpoint, JsonData(
                new PaymentLinkPayByCreditCardRequestDto()
                {
                    CardOwner = _cardOwnerInformation
                }));

            var responseObj = await response.Content.ReadFromJsonAsync<ValidateErrorResult>();
            _scenarioContext["object"] = responseObj;
            _scenarioContext["responseCode"] = response.StatusCode;
        }

        [Then(@"I see in response that card owner information must be alphabetic")]
        public void ThenCardOwnerInformationNumberMustNumeric()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.CardOwner)
                .Errors
                .Any(x => x == ErrorMessagesResources.CardOwnerMustBeAlphabetic).Should().BeTrue();

        }

        [Then(@"I see in response that card owner information can not be null or empty")]
        public void ThenCardOwnerInformationNumberCanNotBeNullOrEmpty()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.CardOwner)
                .Errors
                .Any(x => x == ErrorMessagesResources.CardOwnerCanNotBeNullOrEmpty).Should().BeTrue();
        }

        //[Then(@"I see response status code is BadRequest")]
        //public void ThenISeeResponseStatusCodeIsBadRequest()
        //{
        //    var responseCode = (HttpStatusCode)_scenarioContext["responseCode"];
        //    responseCode.Should().Be(HttpStatusCode.BadRequest);
        //}

    }
}
