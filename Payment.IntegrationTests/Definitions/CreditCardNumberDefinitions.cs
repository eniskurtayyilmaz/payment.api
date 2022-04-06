using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Payment.Api.Validators;
using TechTalk.SpecFlow;

namespace Payment.IntegrationTests.Definitions
{
    [Binding]
    public class CreditCardNumberDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorFactory _validatorFactory;
        private string _cardnumber;


        public CreditCardNumberDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }



        [Given(@"the credit card number is (.*)")]
        public void GivenTheCreditCardNumberIs(string cardnumber)
        {
            _validatorFactory = new ValidatorFactory("");
            _cardnumber = cardnumber;
        }

        [When(@"I set credit card number validations, getting validators")]
        public void WhenISetValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new CardNumberValidator(_cardnumber)
            });

            var validators = _validatorFactory.GetValidators(null);
            _scenarioContext["Result"] = validators;
        }


        [Then(@"the credit card number must invalid")]
        public void ThenCreditCardNumberMustInvalid()
        {

            var validators = _scenarioContext["Result"] as IList<IValidator>;
            validators.Should().NotBeNull();
            validators.Count.Should().Be(1);

            var validator = validators.First();
            validator.Should().NotBeNull();
            validator.Should().BeAssignableTo<CardNumberValidator>();
            validator.Validate().Should().NotBeNull();
            validator.Validate().IsValid.Should().BeFalse();
        }
        
    }
}
