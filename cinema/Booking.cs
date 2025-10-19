using System;
using System.Collections.Generic;

namespace cinema
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ShowTimeId { get; set; }
        public List<string> SeatIds { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public decimal TotalPrice { get; set; }
        public int PaymentId { get; set; }
        public bool IsConfirmed { get; set; }
        public string BookingReference { get; set; }
        public DateTime? ConfirmedAt { get; set; }

        public Booking()
        {
            SeatIds = new List<string>();
            CreatedAt = DateTime.Now;
            ExpiresAt = CreatedAt.AddMinutes(15);
            Status = "Created";
            BookingReference = "BKG-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        public void AddSeat(string seatId) { if (!SeatIds.Contains(seatId)) SeatIds.Add(seatId); Recalculate(); }

        public void RemoveSeat(string seatId) { if (SeatIds.Contains(seatId)) SeatIds.Remove(seatId); Recalculate(); }

        public void Recalculate()
        {
            TotalPrice = SeatIds.Count * 100m;
        }

        public bool Confirm(int paymentId)
        {
            if (Status == "Created" && DateTime.Now <= ExpiresAt)
            {
                IsConfirmed = true;
                Status = "Confirmed";
                PaymentId = paymentId;
                ConfirmedAt = DateTime.Now;
                return true;
            }
            return false;
        }

        public void Expire() { Status = "Expired"; }

        public string GetSummary() => $"{BookingReference} | Seats: {SeatIds.Count} | Total: {TotalPrice:0.00} | Status: {Status}";

        public bool IsExpired() => DateTime.Now > ExpiresAt;
    }
}
