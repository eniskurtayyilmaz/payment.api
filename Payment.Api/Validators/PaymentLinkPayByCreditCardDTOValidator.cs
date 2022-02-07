using System.Text.RegularExpressions;
using FluentValidation;
using Payment.Api.Models;
using Payment.Api.Resources;

namespace Payment.Api.Validators
{
    public class PaymentLinkPayByCreditCardDTOValidator : AbstractValidator<PaymentLinkPayByCreditCardDTO>
    {
        public PaymentLinkPayByCreditCardDTOValidator()
        {
            RuleFor(x => x.CardOwner)
                .NotNull().WithMessage(ErrorMessagesResources.CardOwnerInvalid)
                .NotEmpty().WithMessage(ErrorMessagesResources.CardOwnerInvalid)
                .Matches("^((?:[A-Za-z]+ ?){3})$").WithMessage(ErrorMessagesResources.CardOwnerInvalid);

            RuleFor(x => x.CreditCardNumber)
                .NotNull().WithMessage(ErrorMessagesResources.CreditCardNumberInvalid)
                .NotEmpty().WithMessage(ErrorMessagesResources.CreditCardNumberInvalid)
                .Matches(@"^[0-9]+$").WithMessage(ErrorMessagesResources.CreditCardNumberInvalid)
                .Matches(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13})$").WithMessage(ErrorMessagesResources.CreditCardTypeInvalid);
        }
    }
}