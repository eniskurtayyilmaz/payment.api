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
    public class ExpirationDateDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorHandler _validatorFactory;
        private string _exp;
        private string _cardnumber;


        public ExpirationDateDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the expiration date is (.*)")]
        public void GivenTheExpirationDateIs(string exp)
        {
            var requestModel = new PaymentLinkPayByCreditCardRequestDTO
            {
                IssueDate = _exp
            };
            _validatorFactory = new ValidatorHandler(requestModel);
            _exp = exp;
        }

        [Given(@"the credit card is (.*)")]
        public void GivenTheCreditCardIsCardnumber(string cardnumber)
        {
            _cardnumber = cardnumber;
        }

        [When(@"I set expiration date and credit card validations, getting validators")]
        public void WhenISetExpirationDateAndCreditCardValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new ExpireDateValidator(_exp),
                new CardNumberValidator(_cardnumber),
                new CreditCardTypeFactoryBuilder(_cardnumber).SetDefaultValidators()
            });

        }



        [When(@"I set expiration date validations, getting validators")]
        public void WhenISetValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new ExpireDateValidator(_exp)
            });

         
        }


        [Then(@"Expiration date must invalid")]
        public void ThenExpirationDateMustInvalid()
        {
            var validator = _validatorFactory.Validate();
            validator.Should().NotBeNull();
            validator.IsValid.Should().BeFalse();
            validator.Error.Should().Contain("ExpireDate");
        }
    }
}
