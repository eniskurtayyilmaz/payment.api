namespace Payment.Api.Models
{
    public class PaymentLinkPayByCreditCardDTO
    {
        public string CardOwner { get; set; }
        public string CreditCardNumber { get; set; }
        public string IssueDate { get; set; }
        public string CVC { get; set; }
    }
}