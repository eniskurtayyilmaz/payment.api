namespace Payment.Api.Validators
{
    public class CardNumberValidator : Validator<string>
    {
        public CardNumberValidator(string cardOwnerInformation) : base(cardOwnerInformation)
        {

        }

        public override ValidatorResult IsValid()
        {
            var arg = this.ObjectValue;
            if (string.IsNullOrEmpty(arg) || string.IsNullOrWhiteSpace(arg))
            {
                return new ValidatorResult("Card number not be null");
            }


            return new ValidatorResult(true);



            //return Regex.IsMatch(arg, "^[0-9]{16}$")
            //    ? new ValidatorResult(true)
            //    : new ValidatorResult("Card owner information is invalid");
        }
    }
}