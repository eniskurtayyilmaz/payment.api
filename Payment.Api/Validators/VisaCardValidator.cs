using System.Text.RegularExpressions;
using Payment.Api.Constants;
using Payment.Api.Models;

namespace Payment.Api.Validators
{
    public class VisaCardValidator : Validator<string>
    {
        public VisaCardValidator(string creditCardNumber) : base(creditCardNumber)
        {

        }

        public override ValidatorResult Validate()
        {
            var arg = this.ObjectValue;
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult(PropertyConstants.CreditCard, "The credit card type isn't Visa card");
            }

            return Regex.IsMatch(arg, "^4[0-9]{12}(?:[0-9]{3})?$")
                ? new ValidatorResult(true)
                : new ValidatorResult(PropertyConstants.CreditCard, "The credit card type isn't Visa card");
        }
    }
}