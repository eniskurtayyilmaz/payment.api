using System.Text.RegularExpressions;
using Payment.Api.Constants;
using Payment.Api.Models;

namespace Payment.Api.Validators
{
    public class AmericanExpressCardValidator : Validator<string>
    {
        public AmericanExpressCardValidator(string creditCardNumber) : base(creditCardNumber)
        {

        }

        public override ValidatorResult Validate()
        {
            var arg = this.ObjectValue;
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult(PropertyConstants.CreditCard, "The credit card type isn't Americanexpress");
            }

            return Regex.IsMatch(arg, "^3[47][0-9]{13}$")
                ? new ValidatorResult(true)
                : new ValidatorResult(PropertyConstants.CreditCard, "The credit card type isn't Americanexpress");
        }
    }
}