using System;
using System.Text.RegularExpressions;
using FluentValidation;
using Payment.Api.Models;
using Payment.Api.Resources;
using Payment.Api.Utils;

namespace Payment.Api.Validators
{
    public class PaymentLinkPayByCreditCardRequestDTOValidator : AbstractValidator<PaymentLinkPayByCreditCardRequestDTO>
    {
        public PaymentLinkPayByCreditCardRequestDTOValidator()
        {
            RuleFor(x => x.CardOwner)
                .NotNull().WithMessage(ErrorMessagesResources.CardOwnerInvalid)
                .NotEmpty().WithMessage(ErrorMessagesResources.CardOwnerInvalid)
                .Matches("^((?:[A-Za-z]+ ?){3})$").WithMessage(ErrorMessagesResources.CardOwnerInvalid);

            RuleFor(x => x.CreditCardNumber)
                .NotNull().WithMessage(ErrorMessagesResources.CreditCardNumberInvalid)
                .NotEmpty().WithMessage(ErrorMessagesResources.CreditCardNumberInvalid)
                .CreditCard().WithMessage(ErrorMessagesResources.CreditCardNumberInvalid)
                .Matches(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13})$")
                .WithMessage(ErrorMessagesResources.CreditCardTypeInvalid);

            RuleFor(x => x.IssueDate)
                .NotNull().WithMessage(ErrorMessagesResources.IssueDateInvalid)
                .NotEmpty().WithMessage(ErrorMessagesResources.IssueDateInvalid)
                .Matches(@"^(0[1-9]|1[0-2])\/(2[2-9])$").WithMessage(ErrorMessagesResources.IssueDateInvalid)
                .Must(ValidateIssueDate).WithMessage(ErrorMessagesResources.IssueDateInvalid);
            
            RuleFor(x => x.CVC)
                .NotNull().WithMessage(ErrorMessagesResources.CVCInvalid)
                .NotEmpty().WithMessage(ErrorMessagesResources.CVCInvalid)
                .Matches("^[0-9]{3,4}$").WithMessage(ErrorMessagesResources.CVCInvalid);
        }
        
        private bool ValidateIssueDate(string arg)
        {
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return false;
            }

            var splitDate = arg.Split("/");
            if (splitDate.Length is <= 1 or >= 3)
            {
                return false;
            }

            var currentDateTime = ClockUtils.Now();

            if (!int.TryParse(splitDate[0], out var convertedMonth) ||
                !int.TryParse(splitDate[1], out var convertedYear))
            {
                return false;
            }

            var year = Convert.ToInt32(currentDateTime.ToString("yy"));
            var month = currentDateTime.Month;

            return year <= convertedYear && (year < convertedYear || month < convertedMonth);
        }
    }
}