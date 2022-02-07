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
        }
    }
}