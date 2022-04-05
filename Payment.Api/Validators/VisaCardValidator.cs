using System.Text.RegularExpressions;

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
            return Regex.IsMatch(arg, "^4[0-9]{12}(?:[0-9]{3})?$")
                ? new ValidatorResult(true)
                : new ValidatorResult("The credit card type isn't Visa card");
        }
    }
}