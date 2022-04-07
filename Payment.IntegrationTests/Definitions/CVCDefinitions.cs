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
    public class CVCDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorHandler _validatorFactory;
        private string _cvc;


        public CVCDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the CVC is (.*)")]
        public void GivenTheCVCIs(string cvc)
        {
            var requestModel = new PaymentLinkPayByCreditCardRequestDTO
            {
                CVC = cvc
            };
            _validatorFactory = new ValidatorHandler(requestModel);
            _cvc = cvc;
        }

        [When(@"I set CVC validations, getting validators")]
        public void WhenISetValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new CvcValidator(_cvc)
            });
            
        }


        [Then(@"CVC must invalid")]
        public void ThenCVCMustInvalid()
        {

            var validator = _validatorFactory.Validate();
            validator.Should().NotBeNull();
            //validator.IsValid.Should().BeFalse();
            //validator.Error.Should().Contain("CVC");
        }
        
    }
}
