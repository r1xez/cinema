using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    public class Cinema
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Hall> Halls { get; set; }
        public List<Film> Films { get; set; }
        public List<Genre> Genres { get; set; }
        public List<ShowTime> ShowTimes { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Staff> StaffMembers { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Discount> Discounts { get; set; }

        public Cinema()
        {
            Halls = new List<Hall>();
            Films = new List<Film>();
            Genres = new List<Genre>();
            ShowTimes = new List<ShowTime>();
            Customers = new List<Customer>();
            StaffMembers = new List<Staff>();
            Bookings = new List<Booking>();
            Payments = new List<Payment>();
            Tickets = new List<Ticket>();
            Reviews = new List<Review>();
            Discounts = new List<Discount>();
        }

        public void AddHall(Hall h) { if (!Halls.Contains(h)) Halls.Add(h); }

        public void AddFilm(Film f) { if (!Films.Any(x => x.Id == f.Id)) Films.Add(f); }

        public void AddGenre(Genre g) { if (!Genres.Any(x => x.Id == g.Id)) Genres.Add(g); }

        public ShowTime ScheduleShow(int filmId, int hallId, DateTime start, decimal basePrice, string lang = "UA", string sub = "")
        {
            var film = Films.FirstOrDefault(f => f.Id == filmId);
            var hall = Halls.FirstOrDefault(h => h.Id == hallId);
            if (film == null || hall == null) return null;
            var st = new ShowTime
            {
                Id = ShowTimes.Count + 1,
                Film = film,
                Hall = hall,
                StartTime = start,
                EndTime = start.AddMinutes(film.DurationMinutes),
                BasePrice = basePrice,
                Language = lang,
                Subtitle = sub
            };
            ShowTimes.Add(st);
            return st;
        }

        public Booking CreateBooking(int customerId, int showTimeId, List<string> seatIds)
        {
            var st = ShowTimes.FirstOrDefault(s => s.Id == showTimeId);
            var cust = Customers.FirstOrDefault(c => c.Id == customerId);
            if (st == null || cust == null) return null;
            var booking = new Booking { Id = Bookings.Count + 1, CustomerId = customerId, ShowTimeId = showTimeId };
            foreach (var sid in seatIds)
            {
                var seat = st.Hall.Seats.FirstOrDefault(s => s.Id == sid);
                if (seat != null && seat.IsAvailable)
                {
                    booking.AddSeat(sid);
                    seat.Reserve();
                }
            }
            booking.Recalculate();
            Bookings.Add(booking);
            cust.AddBooking(booking.Id);
            return booking;
        }

        public Ticket PurchaseTicket(int bookingId, int customerId)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);
            var cust = Customers.FirstOrDefault(c => c.Id == customerId);
            if (booking == null || cust == null || booking.IsConfirmed) return null;
            var pay = new Payment { Id = Payments.Count + 1, Amount = booking.TotalPrice, Method = "Wallet", CustomerId = customerId, Refundable = false };
            pay.Process();
            Payments.Add(pay);
            var confirmed = booking.Confirm(pay.Id);
            if (!confirmed) return null;
            foreach (var sid in booking.SeatIds)
            {
                var seat = ShowTimes.First(s => s.Id == booking.ShowTimeId).Hall.Seats.First(ss => ss.Id == sid);
                var t = new Ticket
                {
                    Id = Tickets.Count + 1,
                    ShowTimeId = booking.ShowTimeId,
                    Seat = seat,
                    Price = 100 * seat.PriceModifier,
                    PurchaserCustomerId = customerId
                };
                Tickets.Add(t);
                cust.AddTicket(t.Id);
            }
            return Tickets.LastOrDefault();
        }

        public decimal CalculateTotalPotentialRevenue() => ShowTimes.Sum(s => s.CalculatePotentialRevenue());

        public IEnumerable<ShowTime> FindShowsByFilmTitle(string partial) => ShowTimes.Where(s => s.Film.Title.Contains(partial, StringComparison.OrdinalIgnoreCase));

        public void AddDiscount(Discount d) { if (!Discounts.Contains(d)) Discounts.Add(d); }

        public void AddReview(Review r) { Reviews.Add(r); }
    }
}
