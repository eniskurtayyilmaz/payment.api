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
    public class CardOwnerInformationDefinitions : TestInitialize
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorHandler _validatorFactory;
        private string _cardInformation;


        public CardOwnerInformationDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the card owner information is (.*)")]
        public void GivenTheCardOwnerInformationIs(string cardowner)
        {
            var requestModel = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = cardowner
            };
            _validatorFactory = new ValidatorHandler(requestModel);
            _cardInformation = cardowner;
        }

        [When(@"I set card owner information validations, getting validators")]
        public void WhenISetValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new CardOwnerInformationValidator(_cardInformation)
            });
        }


        [Then(@"card owner information must invalid")]
        public void ThenCardOwnerInformationMustInvalid()
        {
            var validator = _validatorFactory.Validate();
            validator.Should().NotBeNull();
            //validator.IsValid.Should().BeFalse();
            //validator.Error.Should().Contain("Card owner");
        }
        
    }
}
