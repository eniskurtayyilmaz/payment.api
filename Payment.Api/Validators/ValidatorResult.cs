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
        public TModel ObjectValue { get; protected set; }

        public ValidatorResult(TModel objectValue)
        {
            this.ObjectValue = objectValue;
        }

        public abstract ValidateErrorResult Validate();
    }
}