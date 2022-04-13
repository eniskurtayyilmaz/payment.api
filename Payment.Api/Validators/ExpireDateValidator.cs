using System;
using Payment.Api.Constants;
using Payment.Api.Models;
using Payment.Api.Resources;
using Payment.Api.Utils;

namespace Payment.Api.Validators
{
    public class ExpireDateValidator : Validator<string>
    {
        public ExpireDateValidator(string expireDate) : base(expireDate)
        {

        }

        public override ValidatorResult Validate()
        {
            var arg = this.ObjectValue;
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult(PropertyConstants.ExpirationDate, ErrorMessagesResources.ExpirationDateCanNotBeNullOrEmpty);
            }

            var splitDate = arg.Split("/");
            if (splitDate.Length is <= 1 or >= 3)
            {
                //"ExpireDate doesn't containts of '/'"
                return new ValidatorResult(PropertyConstants.ExpirationDate, ErrorMessagesResources.ExpirationDateMustBeMMYYFormat);
            }

            var currentDateTime = ClockUtils.Now();

            if (!int.TryParse(splitDate[0], out var convertedMonth) ||
                !int.TryParse(splitDate[1], out var convertedYear))
            {
                //"ExpireDate doesn't containts of numbers"
                return new ValidatorResult(PropertyConstants.ExpirationDate, ErrorMessagesResources.ExpirationDateMustBeMMYYFormat);
            }

            var year = Convert.ToInt32(currentDateTime.ToString("yy"));
            var month = currentDateTime.Month;

            return year <= convertedYear && (year < convertedYear || month < convertedMonth) ?
                new ValidatorResult() : new ValidatorResult(PropertyConstants.ExpirationDate, ErrorMessagesResources.CreditCardExpirationDateExpired);
        }
    }
}