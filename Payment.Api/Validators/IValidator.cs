using System;

namespace Payment.Api.Validators
{
    public interface IValidator
    {
        ValidatorResult IsValid();
    }

    public abstract class Validator<TModel> : IValidator
    {
        public TModel ObjectValue { get; protected set; }

        public Validator(TModel objectValue)
        {
            this.ObjectValue = objectValue;
        }
        
        public abstract ValidatorResult IsValid();
    }
}