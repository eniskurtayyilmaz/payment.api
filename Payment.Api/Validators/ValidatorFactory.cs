using System;
using System.Collections.Generic;

namespace Payment.Api.Validators
{


    public enum CreditCardType
    {
        MasterCard,
        Visa,
        Amex
    }

    public class ValidatorFactory
    {
        private readonly string _creditCardNumber;

        private IList<IValidator> _validators;
        public ValidatorFactory(string creditCardNumber)
        {
            _creditCardNumber = creditCardNumber;
        }

        public IList<IValidator> GetValidators(CreditCardType? validationType)
        {
            if (_validators == null)
            {
                _validators = new List<IValidator>();
            }

            if (!validationType.HasValue) return _validators;

            switch (validationType)
            {
                case CreditCardType.MasterCard:
                    _validators.Add(new MasterCreditCardValidator(_creditCardNumber));
                    break;

                case CreditCardType.Visa:
                    _validators.Add(new VisaCardValidator(_creditCardNumber));
                    break;

                case CreditCardType.Amex:
                    _validators.Add(new AmericanExpressCardValidator(_creditCardNumber));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _validators;
        }

        public void SetValidators(IList<IValidator> validators)
        {
            this._validators = validators;

            /* 
            this._validators = new List<IValidator>()
            {
                new ExpireDateValidator(this._expireDate),
                new CvcValidator(this._cvc),
                new CardOwnerInformationValidator(this._cardOwnerInformation),
                new CardNumberValidator(this._creditCardNumber)
            };
            */
        }
    }
}