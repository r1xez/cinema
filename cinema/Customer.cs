using System;
using System.Collections.Generic;

namespace cinema
{
    public class Customer : Person
    {
        public string CustomerNumber { get; set; }
        public LoyaltyCard Loyalty { get; set; }
        public List<int> BookingIds { get; set; }
        public decimal WalletBalance { get; set; }
        public List<string> PreferredGenres { get; set; }
        public List<int> TicketIds { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime RegisteredAt { get; set; }
        public decimal TotalSpent { get; set; }
        public bool IsBlocked { get; set; }

        public Customer()
        {
            Role = "Customer";
            BookingIds = new List<int>();
            PreferredGenres = new List<string>();
            TicketIds = new List<int>();
            RegisteredAt = DateTime.Now;
        }

        public void AddBooking(int bookingId) { if (!BookingIds.Contains(bookingId)) BookingIds.Add(bookingId); }

        public void CancelBooking(int bookingId) { if (BookingIds.Contains(bookingId)) BookingIds.Remove(bookingId); }

        public void AddFunds(decimal amount) { WalletBalance += amount; }

        public bool DeductFunds(decimal amount)
        {
            if (WalletBalance >= amount) { WalletBalance -= amount; TotalSpent += amount; return true; }
            return false;
        }

        public void AddTicket(int ticketId) { if (!TicketIds.Contains(ticketId)) TicketIds.Add(ticketId); }

        public bool IsAdult() => GetAge() >= 18;

        public void ApplyLoyaltyPoints(int points)
        {
            Loyalty ??= new LoyaltyCard(this);
            Loyalty.AddPoints(points);
        }

        public void BlockCustomer() { IsBlocked = true; }

        public void UnblockCustomer() { IsBlocked = false; }

        public void AddPreferredGenre(string genre) { if (!PreferredGenres.Contains(genre)) PreferredGenres.Add(genre); }
    }
}
