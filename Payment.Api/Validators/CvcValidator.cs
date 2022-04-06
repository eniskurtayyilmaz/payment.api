using System.Text.RegularExpressions;
using Payment.Api.Models;

namespace Payment.Api.Validators
{
    public class CvcValidator : Validator<string>
    {
        public CvcValidator(string cvc) : base(cvc)
        {
        }

        public override ValidatorResult Validate()
        {
            var arg = this.ObjectValue;
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult("CVC not be null");
            }

            return Regex.IsMatch(arg, "^[0-9]{3,4}$")
                ? new ValidatorResult(true)
                : new ValidatorResult("CVC is invalid");
        }
    }
}