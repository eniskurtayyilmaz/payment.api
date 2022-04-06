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
    public class ValidCreditCardDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private ValidatorHandler _validatorFactory;
        private string _cvc;
        private string _cardowner;
        private string _exp;
        private string _cardnumber;


        public ValidCreditCardDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Given(@"the valid credit number is (.*)")]
        public void GivenTheValidCreditNumberIs(string cardnumber)
        {
            _cardnumber = cardnumber;
        }

        [Given(@"the valid card owner (.*)")]
        public void GivenTheValidCardOwner(string cardowner)
        {
            _cardowner = cardowner;
        }


        [Given(@"the valid expiration date (.*)")]
        public void GivenTheValidExpirationDate(string exp)
        {
            _exp = exp;
        }

        [Given(@"the valid CVC (.*)")]
        public void GivenTheValidCVC(string cvc)
        {
            _cvc = cvc;

        }


        [When(@"I set known credit card validations and other validations")]
        public void WhenISetValidationsGettingValidators()
        {
            var requestModel = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = _cardowner,
                CreditCardNumber = _cardnumber,
                IssueDate = _exp,
                CVC = _cvc
            };
            _validatorFactory = new ValidatorHandler(requestModel);
            _validatorFactory.SetValidators(new List<IValidator>()
            {
                new CardOwnerInformationValidator(_cardowner),
                new CvcValidator(_cvc),
                new ExpireDateValidator(_exp),
                new CardNumberValidator(_cardnumber),
                new CreditCardTypeFactoryBuilder(_cardnumber).SetDefaultValidators()
            });

        }


        [Then(@"all of informatin must be valid")]
        public void ThenMustValid()
        {

            var validator = _validatorFactory.Validate();
            validator.Should().NotBeNull();
            validator.IsValid.Should().BeTrue();
        }

    }
}
