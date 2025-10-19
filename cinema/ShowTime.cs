using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    public class ShowTime
    {
        public int Id { get; set; }
        public Film Film { get; set; }
        public Hall Hall { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AvailableSeats => Hall?.Seats.Count(s => s.IsAvailable) ?? 0;
        public decimal BasePrice { get; set; }
        public string Language { get; set; }
        public string Subtitle { get; set; }
        public bool IsSoldOut => AvailableSeats == 0;
        public List<int> BookingIds { get; set; }
        public bool Is3DSession { get; set; }

        public ShowTime()
        {
            BookingIds = new List<int>();
        }

        public bool BookSeat(Seat seat)
        {
            if (seat == null || !seat.IsAvailable) return false;
            return seat.Reserve();
        }

        public void CancelSeat(Seat seat) { seat?.Release(); }

        public int GetRemainingSeats() => AvailableSeats;

        public void ExtendShow(TimeSpan extra) { EndTime = EndTime.Add(extra); }

        public void ChangePrice(decimal newPrice) { BasePrice = newPrice; }

        public decimal CalculatePotentialRevenue()
        {
            return Hall.Seats.Sum(s => (decimal)s.PriceModifier * BasePrice);
        }

        public IEnumerable<Seat> GetAvailableSeats() => Hall?.Seats.Where(s => s.IsAvailable) ?? new List<Seat>();
    }
}
