using System.Text.RegularExpressions;
using Payment.Api.Constants;
using Payment.Api.Models;
using Payment.Api.Resources;

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
                return new ValidatorResult(PropertyConstants.CreditCard, ErrorMessagesResources.CardNumberCanNotBeNullOrEmpty);
            }


            return Regex.IsMatch(arg, "^[0-9]{15,16}$")
                ? new ValidatorResult()
                : new ValidatorResult(PropertyConstants.CreditCard, ErrorMessagesResources.CardNumberMustBeNumericWith15_16Length);
        }
    }
}