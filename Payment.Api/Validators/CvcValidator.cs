using System.Text.RegularExpressions;
using Payment.Api.Constants;
using Payment.Api.Models;
using Payment.Api.Resources;

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
                return new ValidatorResult(PropertyConstants.CVC, ErrorMessagesResources.CVCCanNotBeNullOrEmpty);
            }

            return Regex.IsMatch(arg, "^[0-9]{3,4}$")
                ? new ValidatorResult()
                : new ValidatorResult(PropertyConstants.CVC, ErrorMessagesResources.CVCMustBeNumericWith3_4Length);
        }
    }
}