using System;
using System.Collections.Generic;
using Payment.Api.Models;

namespace Payment.Api.Validators
{
    public class ValidatorHandler : Validator<PaymentLinkPayByCreditCardRequestDTO>
    {
        private readonly PaymentLinkPayByCreditCardRequestDTO _model;

        private IList<IValidator> _validators;
        public ValidatorHandler(PaymentLinkPayByCreditCardRequestDTO model) : base(model)
        {
            _model = model;
        }

        public IList<IValidator> GetValidators()
        {
            return _validators;
        }

        public void SetValidators(IList<IValidator> validators)
        {
            this._validators = validators;
        }

        public override ValidatorResult Validate()
        {
            foreach (var validator in _validators)
            {
                var result = validator.Validate();
                if (!result.IsValid)
                {
                    return result;
                }
            }
            return base.Validate();
        }
    }
}