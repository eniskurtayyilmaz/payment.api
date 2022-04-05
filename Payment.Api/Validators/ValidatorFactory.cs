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
        private readonly string _cardOwnerInformation;
        private readonly string _creditCardNumber;
        private readonly string _expireDate;
        private readonly string _cvc;

        public ValidatorFactory(string cardOwnerInformation, string creditCardNumber, string expireDate, string cvc)
        {
            _cardOwnerInformation = cardOwnerInformation;
            _creditCardNumber = creditCardNumber;
            _expireDate = expireDate;
            _cvc = cvc;
        }

        public IList<IValidator> GetValidators(CreditCardType validationType)
        {
            IList<IValidator> validators = GetDefaultValidators();

            switch (validationType)
            {
                case CreditCardType.MasterCard:
                    validators.Add(new MasterCreditCardValidator(_creditCardNumber));
                    break;

                case CreditCardType.Visa:
                    validators.Add(new VisaCardValidator(_creditCardNumber));
                    break;

                case CreditCardType.Amex:
                    validators.Add(new AmericanExpressCardValidator(_creditCardNumber));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(validationType), validationType, null);
            }

            return validators;
        }

        public IList<IValidator> GetDefaultValidators()
        {
            return new List<IValidator>()
            {
                new ExpireDateValidator(this._expireDate),
                new CvcValidator(this._cvc),
                new CardOwnerInformationValidator(this._cardOwnerInformation),
                new CardNumberValidator(this._creditCardNumber)
            };
        }
    }
}