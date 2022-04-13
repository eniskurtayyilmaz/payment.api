using System;

namespace Payment.Api.Models
{
    public class ValidatorResult
    {
        public bool IsValid { get; set; }
        public string Property { get; set; }
        public string Error { get; set; }

        public ValidatorResult(string property, string error)
        {
            Property = property;
            Error = error;
        }

        public ValidatorResult()
        {
            IsValid = true;
        }
    }
}