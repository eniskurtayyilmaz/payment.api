namespace Payment.Api.Validators
{
    public class ValidatorResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }

        public ValidatorResult(string error)
        {
            Error = error;
        }

        public ValidatorResult(bool isValid, string error = "")
        {
            IsValid = isValid;
            Error = error;
        }
    }
}