using System.Collections.Generic;

namespace Payment.Api.Models
{
    public class ValidateError
    {
        public ValidateError(string property, List<string> errors)
        {
            Property = property;
            Errors = errors;
        }

        public string Property { get; set; }
        public List<string> Errors { get; set; }
    }
}