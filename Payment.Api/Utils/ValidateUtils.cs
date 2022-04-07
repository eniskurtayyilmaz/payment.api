using System.Collections.Generic;
using System.Linq;
using Payment.Api.Models;
using Payment.Api.Validators;

namespace Payment.Api.Utils
{
    public class ValidateUtils
    {
        public ValidateErrorResult GetValidateErrorResult(IList<IValidator> validators)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

            foreach (var item in validators)
            {
                var resultValidate = item.Validate();
                if (!resultValidate.IsValid)
                {
                    if (dictionary.ContainsKey(resultValidate.Property))
                    {
                        dictionary[resultValidate.Property].Add(resultValidate.Error);
                    }
                    else
                    {
                        dictionary.Add(resultValidate.Property, new List<string>()
                        {
                            resultValidate.Error
                        });
                    }
                }
            }

            return dictionary.Count <= 0
                ? null
                : new ValidateErrorResult(dictionary.Select(x => new ValidateError(x.Key, x.Value)).ToList());
        }
    }
}
