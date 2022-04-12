using System.Collections.Generic;
using Payment.Api.Constants;
using Payment.Api.Models;
using Payment.Api.Resources;

namespace Payment.Api.Validators
{
    public class CreditCardTypeFactoryBuilder : Validator<string>
    {
        private readonly string _cardNumber;
        private IList<IValidator> _validators;
        public CreditCardTypeFactoryBuilder(string cardNumber) : base(cardNumber)
        {
            _cardNumber = cardNumber;
            _validators = new List<IValidator>();
        }

        public CreditCardTypeFactoryBuilder SetDefaultValidators()
        {
            _validators = new List<IValidator>();
            _validators.Add(new MasterCreditCardValidator(_cardNumber));
            _validators.Add(new VisaCardValidator(_cardNumber));
            _validators.Add(new AmericanExpressCardValidator(_cardNumber));

            return this;
        }

        public override ValidatorResult Validate()
        {
            foreach (var validator in _validators)
            {
                var result = validator.Validate();
                if (result.IsValid)
                {
                    return result;
                }
            }

            return new ValidatorResult(PropertyConstants.CreditCard, ErrorMessagesResources.CreditCardOnlyAcceptedCards);
        }
    }
}