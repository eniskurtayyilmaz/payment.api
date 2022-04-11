using System;
using System.Collections.Generic;
using Payment.Api.Models;

namespace Payment.Api.Validators
{

    public interface IValidatorErrorResult
    {
        ValidateErrorResult Validate();
    }

    public abstract class ValidatorResult<TModel> : IValidatorErrorResult
    {
        protected ValidatorResult(TModel objectValue)
        {

        }

        public abstract ValidateErrorResult Validate();
    }
}