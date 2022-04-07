using System;
using Payment.Api.Models;

namespace Payment.Api.Validators
{
    

    public interface IValidator
    {
        ValidatorResult Validate();
    }

    public abstract class Validator<TModel> : IValidator
    {
        public TModel ObjectValue { get; protected set; }

        public Validator(TModel objectValue)
        {
            this.ObjectValue = objectValue;
        }

        public abstract ValidatorResult Validate();
    }
}