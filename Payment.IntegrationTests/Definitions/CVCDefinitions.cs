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
    public class CVCDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorFactory _validatorFactory;
        private string _cvc;


        public CVCDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the CVC is (.*)")]
        public void GivenTheCVCIs(string cvc)
        {
            _validatorFactory = new ValidatorFactory("");
            _cvc = cvc;
        }

        [When(@"I set validations, getting validators")]
        public void WhenISetValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new CvcValidator(_cvc)
            });

            var validators = _validatorFactory.GetValidators(null);
            _scenarioContext["Result"] = validators;
        }


        [Then(@"CVC must invalid")]
        public void ThenCVCMustInvalid()
        {

            var validators = _scenarioContext["Result"] as IList<IValidator>;
            validators.Should().NotBeNull();
            validators.Count.Should().Be(1);

            var cvcValidator = validators.First();
            cvcValidator.Should().NotBeNull();
            cvcValidator.Validate().Should().NotBeNull();
            cvcValidator.Validate().IsValid.Should().BeFalse();
        }
        
    }
}
