using System;

namespace cinema
{
    // Клас Payment представляє платіж у кінотеатрі
    public class Payment
    {
        // Унікальний ідентифікатор платежу
        public int Id { get; set; }

        // Сума платежу
        public decimal Amount { get; set; }

        // Метод оплати (наприклад: "Card", "Wallet", "Cash")
        public string Method { get; set; }

        // Дата і час обробки платежу
        public DateTime? ProcessedAt { get; set; }

        // Чи був платіж успішним
        public bool IsSuccessful { get; private set; }

        // Унікальний ідентифікатор транзакції
        public string TransactionId { get; private set; }

        // Провайдер платіжної системи
        public string PaymentProvider { get; set; }

        // Валюта платежу
        public string Currency { get; set; }

        // Комісія за платіж
        public decimal Fee { get; private set; }

        // Чи можна зробити повернення коштів
        public bool Refundable { get; set; }

        // Id клієнта, який здійснив платіж
        public int CustomerId { get; set; }

        // Конструктор за замовчуванням
        public Payment()
        {
            Currency = "UAH";
            Fee = 0;
        }

        // Обробка платежу
        public bool Process(string provider = "DefaultPay")
        {
            PaymentProvider = provider;
            ProcessedAt = DateTime.Now;
            Fee = CalculateFee(); // обчислюємо комісію
            TransactionId = "TX-" + Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
            IsSuccessful = Amount > 0; // платіж успішний, якщо сума > 0
            return IsSuccessful;
        }

        // Обчислення комісії (2% від суми платежу)
        public decimal CalculateFee() => Math.Round(Amount * 0.02m, 2);

        // Повернення коштів
        public bool Refund()
        {
            if (!IsSuccessful || !Refundable) return false;
            IsSuccessful = false;
            TransactionId = "RF-" + Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
            return true;
        }

        // Перевірка, чи достатньо коштів у гаманці
        public bool IsEnoughFunds(decimal walletBalance) => walletBalance >= (Amount + Fee);

        // Встановлення власного TransactionId
        public void SetTransactionId(string tx) { TransactionId = tx; }
    }
}
