namespace Payment.Api.Models
{
    public class PaymentLinkPayByCreditCardRequestDto
    {
        public string CardOwner { get; set; }
        public string CreditCardNumber { get; set; }
        public string IssueDate { get; set; }
        public string CVC { get; set; }
    }
}