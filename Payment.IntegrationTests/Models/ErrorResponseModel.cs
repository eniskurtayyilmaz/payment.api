using System.Collections.Generic;

namespace Payment.IntegrationTests.Models
{
    public class Errors
    {
        public List<string> CVC { get; set; }
        public List<string> CardOwner { get; set; }
        public List<string> IssueDate { get; set; }
        public List<string> CreditCardNumber { get; set; }
    }

    public class ErrorResponseModel
    {
        public Errors errors { get; set; }
    }
}