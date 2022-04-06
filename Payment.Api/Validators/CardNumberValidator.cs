using System.Text.RegularExpressions;

namespace Payment.Api.Validators
{
    public class CardNumberValidator : Validator<string>
    {
        public CardNumberValidator(string cardNumber) : base(cardNumber)
        {

        }

        public override ValidatorResult Validate()
        {
            var arg = this.ObjectValue;
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult("Card number not be null");
            }

            
            return Regex.IsMatch(arg, "^[0-9]{15,16}$")
                ? new ValidatorResult(true)
                : new ValidatorResult("Card owner information is invalid");
        }
    }
}