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
        public void ThenCVCMustInvalid()
        {

            var validators = _scenarioContext["Result"] as IList<IValidator>;
            validators.Should().NotBeNull();
            validators.Count.Should().Be(1);

            var validator = validators.First();
            validator.Should().NotBeNull();
            validator.Should().BeAssignableTo<ExpireDateValidator>();
            validator.Validate().Should().NotBeNull();
            validator.Validate().IsValid.Should().BeFalse();
        }
    }
}
