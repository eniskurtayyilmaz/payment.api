using System.Text.RegularExpressions;

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
            return Regex.IsMatch(arg, "^3[47][0-9]{13}$")
                ? new ValidatorResult(true)
                : new ValidatorResult("The credit card type isn't Americanexpress");
        }
    }
}