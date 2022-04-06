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
    public class CardOwnerInformationDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorFactory _validatorFactory;
        private string _cardInformation;


        public CardOwnerInformationDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the card owner information is (.*)")]
        public void GivenTheCardOwnerInformationIs(string cardowner)
        {
            _validatorFactory = new ValidatorFactory("");
            _cardInformation = cardowner;
        }

        [When(@"I set card owner information validations, getting validators")]
        public void WhenISetValidationsGettingValidators()
        {
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new CardOwnerInformationValidator(_cardInformation)
            });

            var validators = _validatorFactory.GetValidators(null);
            _scenarioContext["Result"] = validators;
        }


        [Then(@"card owner information must invalid")]
        public void ThenCardOwnerInformationMustInvalid()
        {

            var validators = _scenarioContext["Result"] as IList<IValidator>;
            validators.Should().NotBeNull();
            validators.Count.Should().Be(1);

            var validator = validators.First();
            validator.Should().NotBeNull();
            validator.Should().BeAssignableTo<CardOwnerInformationValidator>();
            validator.Validate().Should().NotBeNull();
            validator.Validate().IsValid.Should().BeFalse();
        }
        
    }
}
