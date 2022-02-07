namespace Payment.Api.Models
{
    public class PaymentLinkPayByCreditCardRequestDTO
    {
        public string CardOwner { get; set; }
        public string CreditCardNumber { get; set; }
        public string IssueDate { get; set; }
        public string CVC { get; set; }
    }
}