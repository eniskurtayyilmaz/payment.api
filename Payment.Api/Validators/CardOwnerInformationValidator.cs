using System.Text.RegularExpressions;

namespace Payment.Api.Validators
{
    public class CardOwnerInformationValidator : Validator<string>
    {
        public CardOwnerInformationValidator(string cardOwnerInformation) : base(cardOwnerInformation)
        {

        }

        public override ValidatorResult IsValid()
        {
            var arg = this.ObjectValue;
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult("Card owner information not be null");
            }


         

            return Regex.IsMatch(arg, "^((?:[A-Za-z]+ ?){3})$")
                ? new ValidatorResult(true)
                : new ValidatorResult("Card owner information is invalid");
        }
    }
}