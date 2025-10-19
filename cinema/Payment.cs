using System;

namespace cinema
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public bool IsSuccessful { get; private set; }
        public string TransactionId { get; private set; }
        public string PaymentProvider { get; set; }
        public string Currency { get; set; }
        public decimal Fee { get; private set; }
        public bool Refundable { get; set; }
        public int CustomerId { get; set; }

        public Payment()
        {
            Currency = "UAH";
            Fee = 0;
        }

        public bool Process(string provider = "DefaultPay")
        {
            PaymentProvider = provider;
            ProcessedAt = DateTime.Now;
            Fee = CalculateFee();
            TransactionId = "TX-" + Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
            IsSuccessful = Amount > 0;
            return IsSuccessful;
        }

        public decimal CalculateFee() => Math.Round(Amount * 0.02m, 2);

        public bool Refund()
        {
            if (!IsSuccessful || !Refundable) return false;
            IsSuccessful = false;
            TransactionId = "RF-" + Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
            return true;
        }

        public bool IsEnoughFunds(decimal walletBalance) => walletBalance >= (Amount + Fee);

        public void SetTransactionId(string tx) { TransactionId = tx; }
    }
}
