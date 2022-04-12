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
    public class CVCDefinitions : TestInitialize
    {
        private readonly ScenarioContext _scenarioContext;
        private string _cvc;

        public CVCDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the CVC is (.*) from Examples")]
        public void GivenTheCVCNumberIs(string cvc)
        {
            _cvc = cvc;
        }

        [Given(@"the CVC is empty")]
        public void GivenTheCVCNumberIsEmpty()
        {
            _cvc = string.Empty;
        }

        [Given(@"the CVC is null")]
        public void GivenTheCVCNumberIsNull()
        {
            _cvc = null;
        }

        [When(@"I call the API /api/paymentLink with CVC")]
        public async Task WhenISetValidationsGettingValidators()
        {

            var response = await this.Client.PostAsync(Constant.PaymentLinkEndpoint, JsonData(
                new PaymentLinkPayByCreditCardRequestDto()
                {
                    CVC = _cvc
                }));

            var responseObj = await response.Content.ReadFromJsonAsync<ValidateErrorResult>();
            _scenarioContext["object"] = responseObj;
            _scenarioContext["responseCode"] = response.StatusCode;
        }

        [Then(@"I see in response that CVC must be numeric with 3-4 length")]
        public void ThenCVCNumberMustNumeric()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.CVC)
                .Errors
                .Any(x => x == ErrorMessagesResources.CVCMustBeNumericWith3_4Length).Should().BeTrue();

        }

        [Then(@"I see in response that CVC can not be null or empty")]
        public void ThenCVCNumberCanNotBeNullOrEmpty()
        {
            var responseObj = _scenarioContext["object"] as ValidateErrorResult;
            responseObj.Should().NotBeNull();
            responseObj.Errors.Should().NotBeNull();
            responseObj.Errors.First(x => x.Property == PropertyConstants.CVC)
                .Errors
                .Any(x => x == ErrorMessagesResources.CVCCanNotBeNullOrEmpty).Should().BeTrue();
        }

        //[Then(@"I see response status code is BadRequest")]
        //public void ThenISeeResponseStatusCodeIsBadRequest()
        //{
        //    var responseCode = (HttpStatusCode)_scenarioContext["responseCode"];
        //    responseCode.Should().Be(HttpStatusCode.BadRequest);
        //}

    }
}
