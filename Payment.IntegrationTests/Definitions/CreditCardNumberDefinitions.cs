using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Payment.Api.Models;
using Payment.Api.Validators;
using TechTalk.SpecFlow;

namespace Payment.IntegrationTests.Definitions
{
    [Binding]
    public class CreditCardNumberDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorHandler _validatorFactory;
        private string _cardnumber;


        public CreditCardNumberDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }



        [Given(@"the credit card number is (.*)")]
        public void GivenTheCreditCardNumberIs(string cardnumber)
        {
            var requestModel = new PaymentLinkPayByCreditCardRequestDTO
            {
                CreditCardNumber = cardnumber
            };
            _validatorFactory = new ValidatorHandler(requestModel);
            _cardnumber = cardnumber;
        }

        [When(@"I set credit card number validations, getting validators")]
        public void WhenISetValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new CardNumberValidator(_cardnumber)
            });

        }


        [Then(@"the credit card number must invalid")]
        public void ThenCreditCardNumberMustInvalid()
        {
            var validator = _validatorFactory.Validate();
            validator.Should().NotBeNull();
            validator.IsValid.Should().BeFalse();
            validator.Error.Should().Contain("Card number");
        }
        
    }
}
