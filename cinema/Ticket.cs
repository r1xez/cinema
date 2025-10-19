using System;
using System.Text;

namespace cinema
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ShowTimeId { get; set; }
        public Seat Seat { get; set; }
        public decimal Price { get; set; }
        public int PurchaserCustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsCancelled { get; private set; }
        public string TicketType { get; set; }
        public decimal DiscountApplied { get; set; }
        public string Barcode { get; private set; }
        public string Notes { get; set; }

        public Ticket()
        {
            PurchaseDate = DateTime.Now;
            IsCancelled = false;
            Barcode = GenerateBarcodeInternal();
        }

        public bool ValidateAge(int age, int minAge)
        {
            return age >= minAge;
        }

        public decimal GetFinalPrice() => Math.Max(0, Price - DiscountApplied);

        public void Cancel(string reason = null)
        {
            IsCancelled = true;
            Notes = reason ?? "Cancelled";
            Seat?.Release();
        }

        private string GenerateBarcodeInternal() => Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16).ToUpper();

        public void RegenerateBarcode() { Barcode = GenerateBarcodeInternal(); }

        public string PrintTicket()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"TICKET #{Id} for show {ShowTimeId}");
            sb.AppendLine($"Seat: {Seat?.Row}-{Seat?.Number}  Price: {GetFinalPrice():0.00}");
            sb.AppendLine($"Barcode: {Barcode}");
            return sb.ToString();
        }

        public void ApplyDiscount(decimal amount) { DiscountApplied = Math.Min(amount, Price); }
    }
}
