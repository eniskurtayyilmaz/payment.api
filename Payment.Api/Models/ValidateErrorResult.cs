using System.Collections.Generic;

namespace Payment.Api.Models
{
    public class ValidateErrorResult
    {
        public List<ValidateError> Errors { get; set; }

        public ValidateErrorResult()
        {

        }

        public ValidateErrorResult(List<ValidateError> errors)
        {
            Errors = errors;
        }
    }
}