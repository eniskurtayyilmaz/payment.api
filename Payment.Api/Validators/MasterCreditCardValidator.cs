using System.Text.RegularExpressions;

namespace Payment.Api.Validators
{
    public class MasterCreditCardValidator : Validator<string>
    {
        public MasterCreditCardValidator(string creditCardNumber) : base(creditCardNumber)
        {

        }

        public override ValidatorResult Validate()
        {
            var arg = this.ObjectValue;
            return Regex.IsMatch(arg, "^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$")
                ? new ValidatorResult(true)
                : new ValidatorResult("The credit card type isn't Mastercard");
        }
    }
}