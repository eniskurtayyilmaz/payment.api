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
    public class UnknownCreditCardDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorHandler _validatorFactory;
        private string _cardnumber;


        public UnknownCreditCardDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the unknown credit card is (.*)")]
        public void GivenTheUnkownCreditCardIs(string creditcard)
        {
            var requestModel = new PaymentLinkPayByCreditCardRequestDTO
            {
               CreditCardNumber = creditcard
            };
            _validatorFactory = new ValidatorHandler(requestModel);
            _cardnumber = creditcard;
        }

        [When(@"I set known credit card validations, getting validators")]
        public void WhenISetKnownCreditCardValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new CreditCardTypeFactoryBuilder(_cardnumber).SetDefaultValidators()
            });

        }



        [Then(@"the unknown credit card must invalid")]
        public void ThenExpirationDateMustInvalid()
        {
            var validator = _validatorFactory.Validate();
            validator.Should().NotBeNull();
            validator.IsValid.Should().BeFalse();
            validator.Error.Should().Contain("American Express, Visa, or Mastercard accepted");
        }
    }
}
