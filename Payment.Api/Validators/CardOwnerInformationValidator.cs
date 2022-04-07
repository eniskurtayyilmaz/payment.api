using System.Text.RegularExpressions;
using Payment.Api.Constants;
using Payment.Api.Models;

namespace Payment.Api.Validators
{
    public class CardOwnerInformationValidator : Validator<string>
    {
        public CardOwnerInformationValidator(string cardOwnerInformation) : base(cardOwnerInformation)
        {

        }

        public override ValidatorResult Validate()
        {
            var arg = this.ObjectValue;
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult(PropertyConstants.CardOwner,"Card owner information not be null");
            }


         

            return Regex.IsMatch(arg, "^((?:[A-Za-z]+ ?){3})$")
                ? new ValidatorResult(true)
                : new ValidatorResult(PropertyConstants.CardOwner, "Card owner information is invalid");
        }
    }
}