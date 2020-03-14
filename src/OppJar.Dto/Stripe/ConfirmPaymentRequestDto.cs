namespace OppJar.Dto
{
    public class ConfirmPaymentRequestDto
    {
        public string PaymentMethodId { get; set; }
        public string PaymentIntentId { get; set; }
        public string CustomerId { get; set; }
    }
}
