using System;
using System.Text;

namespace cinema
{
    // Клас, що описує квиток на сеанс
    public class Ticket
    {
        public int Id { get; set; }                    // Унікальний ідентифікатор квитка
        public int ShowTimeId { get; set; }            // Ідентифікатор сеансу, на який куплений квиток
        public Seat Seat { get; set; }                 // Місце у залі
        public decimal Price { get; set; }             // Базова ціна квитка
        public int PurchaserCustomerId { get; set; }   // Ідентифікатор покупця
        public DateTime PurchaseDate { get; set; }     // Дата та час покупки
        public bool IsCancelled { get; private set; }  // Статус скасування
        public string TicketType { get; set; }         // Тип квитка (звичайний, VIP, студент тощо)
        public decimal DiscountApplied { get; set; }   // Сума знижки, застосованої до квитка
        public string Barcode { get; private set; }    // Унікальний штрихкод квитка
        public string Notes { get; set; }              // Додаткові нотатки, наприклад причина скасування

        // Конструктор
        public Ticket()
        {
            PurchaseDate = DateTime.Now;               // Дата покупки встановлюється автоматично
            IsCancelled = false;
            Barcode = GenerateBarcodeInternal();       // Генерується унікальний штрихкод
        }

        // Перевірка віку для обмежень по віку (minAge)
        public bool ValidateAge(int age, int minAge)
        {
            return age >= minAge;
        }

        // Остаточна ціна після знижки
        public decimal GetFinalPrice() => Math.Max(0, Price - DiscountApplied);

        // Скасування квитка
        public void Cancel(string reason = null)
        {
            IsCancelled = true;
            Notes = reason ?? "Cancelled";            // Додаємо нотатку про причину
            Seat?.Release();                           // Звільняємо місце
        }

        // Внутрішня функція генерації унікального штрихкоду
        private string GenerateBarcodeInternal() => Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16).ToUpper();

        // Оновлення штрихкоду
        public void RegenerateBarcode() { Barcode = GenerateBarcodeInternal(); }

        // Форматований друк квитка
        public string PrintTicket()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"TICKET #{Id} for show {ShowTimeId}");
            sb.AppendLine($"Seat: {Seat?.Row}-{Seat?.Number}  Price: {GetFinalPrice():0.00}");
            sb.AppendLine($"Barcode: {Barcode}");
            return sb.ToString();
        }

        // Застосування знижки
        public void ApplyDiscount(decimal amount) { DiscountApplied = Math.Min(amount, Price); }
    }
}
