using System.Text.RegularExpressions;
using Payment.Api.Constants;
using Payment.Api.Models;

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
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult(PropertyConstants.CreditCard, "The credit card type isn't Mastercard");
            }


            return Regex.IsMatch(arg, "^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$")
                ? new ValidatorResult()
                : new ValidatorResult(PropertyConstants.CreditCard, "The credit card type isn't Mastercard");
        }
    }
}