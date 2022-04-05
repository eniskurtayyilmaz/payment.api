using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.TestHelper;
using Payment.Api.Models;
using Payment.Api.Validators;
using TechTalk.SpecFlow;

namespace Payment.IntegrationTests
{
    [Binding]
    public class CreditCardDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly PaymentLinkPayByCreditCardRequestDTOValidator _validator;


        public CreditCardDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._validator = new PaymentLinkPayByCreditCardRequestDTOValidator();
        }

        [Given(@"the card owner is (.*)")]
        public void GivenTheCardOwnerIs(string cardOwner)
        {
            _scenarioContext["PaymentLinkPayByCreditCardRequestDTO"] = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = cardOwner
            };
        }

        [When(@"I request with card number")]
        public async Task WhenIRequestWithCardNumber()
        {
            var model = (PaymentLinkPayByCreditCardRequestDTO) _scenarioContext["PaymentLinkPayByCreditCardRequestDTO"];
            var result = await _validator.TestValidateAsync(model);
            _scenarioContext["Result"] = result.Errors.Any(x=> x.PropertyName == "CardOwner");
        }

        [Then(@"the result must invalid")]
        public void ThenTheResultInvalid()
        {
            var result = (bool) _scenarioContext["Result"];
            result.Should().BeTrue();
        }
    }
}