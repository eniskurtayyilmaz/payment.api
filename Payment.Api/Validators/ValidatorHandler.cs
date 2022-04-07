using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Payment.Api.Models;
using Payment.Api.Utils;

namespace Payment.Api.Validators
{
    public class ValidatorHandler : ValidatorResult<PaymentLinkPayByCreditCardRequestDTO>
    {
        private readonly PaymentLinkPayByCreditCardRequestDTO _model;

        private IList<IValidator> _validators;
        public ValidatorHandler(PaymentLinkPayByCreditCardRequestDTO model) : base(model)
        {
            _model = model;
            this.SetValidators();
        }

        public void SetValidators()
        {
            this.SetValidators(new List<IValidator>()
            {
                new CardOwnerInformationValidator(_model.CardOwner),
                   new CvcValidator(_model.CVC),
                   new ExpireDateValidator(_model.IssueDate),
                   new CardNumberValidator(_model.CreditCardNumber),
                   new CreditCardTypeFactoryBuilder(_model.CreditCardNumber).SetDefaultValidators()
            });
        }

        public void SetValidators(IList<IValidator> validators)
        {
            this._validators = validators;
        }


        public override ValidateErrorResult Validate()
        {
            return new ValidateUtils().GetValidateErrorResult(this._validators);
        }
    }
}