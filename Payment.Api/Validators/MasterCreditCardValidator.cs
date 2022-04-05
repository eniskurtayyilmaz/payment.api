using System.Text.RegularExpressions;

namespace Payment.Api.Validators
{
    public class MasterCreditCardValidator : Validator<string>
    {
        public MasterCreditCardValidator(string creditCardNumber) : base(creditCardNumber)
        {

        }

        public override ValidatorResult IsValid()
        {
            var arg = this.ObjectValue;
            return Regex.IsMatch(arg, "^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$")
                ? new ValidatorResult(true)
                : new ValidatorResult("The credit card type isn't Mastercard");
        }
    }
}