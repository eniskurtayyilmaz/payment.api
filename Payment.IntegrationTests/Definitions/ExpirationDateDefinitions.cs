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
    public class ExpirationDateDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorFactory _validatorFactory;
        private string _exp;
        private string _cardnumber;


        public ExpirationDateDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the expiration date is (.*)")]
        public void GivenTheExpirationDateIs(string exp)
        {
            _validatorFactory = new ValidatorFactory("");
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
                new CardNumberValidator(_cardnumber)
            });

            var validators = _validatorFactory.GetValidators(null);
            _scenarioContext["Result"] = validators;
        }



        [When(@"I set expiration date validations, getting validators")]
        public void WhenISetValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new ExpireDateValidator(_exp)
            });

            var validators = _validatorFactory.GetValidators(null);
            _scenarioContext["Result"] = validators;
        }


        [Then(@"Expiration date must invalid")]
        public void ThenExpirationDateMustInvalid()
        {

            var validators = _scenarioContext["Result"] as IList<IValidator>;
            validators.Should().NotBeNull();

            var validator = validators.First(x => x.GetType() == typeof(ExpireDateValidator));
            validator.Should().NotBeNull();
            validator.Should().BeAssignableTo<ExpireDateValidator>();
            validator.Validate().Should().NotBeNull();
            validator.Validate().IsValid.Should().BeFalse();
        }
    }
}
