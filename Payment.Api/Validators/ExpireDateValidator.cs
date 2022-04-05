using System;
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
                return new ValidatorResult("ExpireDate not be null");
            }

            var splitDate = arg.Split("/");
            if (splitDate.Length is <= 1 or >= 3)
            {
                return new ValidatorResult("ExpireDate doesn't containts of '/'");
            }

            var currentDateTime = ClockUtils.Now();

            if (!int.TryParse(splitDate[0], out var convertedMonth) ||
                !int.TryParse(splitDate[1], out var convertedYear))
            {
                return new ValidatorResult("ExpireDate doesn't containts of numbers");
            }

            var year = Convert.ToInt32(currentDateTime.ToString("yy"));
            var month = currentDateTime.Month;

            return year <= convertedYear && (year < convertedYear || month < convertedMonth) ?
                new ValidatorResult(true) : new ValidatorResult("ExpireDate is invalid");
        }
    }
}