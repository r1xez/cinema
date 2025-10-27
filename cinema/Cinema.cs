using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    // Клас Cinema представляє основну сутність кінотеатру — містить усі дані про фільми, сеанси, клієнтів тощо
    public class Cinema
    {
        // Назва кінотеатру
        public string Name { get; set; }

        // Адреса кінотеатру
        public string Address { get; set; }

        // Список залів
        public List<Hall> Halls { get; set; }

        // Список фільмів, що демонструються у кінотеатрі
        public List<Film> Films { get; set; }

        // Список жанрів фільмів
        public List<Genre> Genres { get; set; }

        // Розклад сеансів
        public List<ShowTime> ShowTimes { get; set; }

        // Список клієнтів
        public List<Customer> Customers { get; set; }

        // Список працівників кінотеатру
        public List<Staff> StaffMembers { get; set; }

        // Список бронювань
        public List<Booking> Bookings { get; set; }

        // Список платежів
        public List<Payment> Payments { get; set; }

        // Список квитків
        public List<Ticket> Tickets { get; set; }

        // Список відгуків користувачів
        public List<Review> Reviews { get; set; }

        // Список знижок та акцій
        public List<Discount> Discounts { get; set; }

        // Конструктор — ініціалізує всі списки
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

        // Додає зал, якщо його ще немає в списку
        public void AddHall(Hall h) { if (!Halls.Contains(h)) Halls.Add(h); }

        // Додає фільм, якщо фільм із таким Id ще не додано
        public void AddFilm(Film f) { if (!Films.Any(x => x.Id == f.Id)) Films.Add(f); }

        // Додає новий жанр, якщо його ще немає
        public void AddGenre(Genre g) { if (!Genres.Any(x => x.Id == g.Id)) Genres.Add(g); }

        // Планує новий сеанс (ShowTime) для фільму
        public ShowTime ScheduleShow(int filmId, int hallId, DateTime start, decimal basePrice, string lang = "UA", string sub = "")
        {
            var film = Films.FirstOrDefault(f => f.Id == filmId); // шукаємо фільм за Id
            var hall = Halls.FirstOrDefault(h => h.Id == hallId); // шукаємо зал за Id
            if (film == null || hall == null) return null; // якщо немає — нічого не створюємо

            // Створюємо новий сеанс
            var st = new ShowTime
            {
                Id = ShowTimes.Count + 1,
                Film = film,
                Hall = hall,
                StartTime = start,
                EndTime = start.AddMinutes(film.DurationMinutes), // кінець сеансу = початок + тривалість фільму
                BasePrice = basePrice,
                Language = lang,
                Subtitle = sub
            };
            ShowTimes.Add(st); // додаємо у список розкладу
            return st;
        }

        // Створює бронювання на сеанс
        public Booking CreateBooking(int customerId, int showTimeId, List<string> seatIds)
        {
            var st = ShowTimes.FirstOrDefault(s => s.Id == showTimeId); // знаходимо сеанс
            var cust = Customers.FirstOrDefault(c => c.Id == customerId); // знаходимо клієнта
            if (st == null || cust == null) return null; // якщо немає — не створюємо

            var booking = new Booking { Id = Bookings.Count + 1, CustomerId = customerId, ShowTimeId = showTimeId };

            // Перевіряємо, чи доступні місця і бронюємо
            foreach (var sid in seatIds)
            {
                var seat = st.Hall.Seats.FirstOrDefault(s => s.Id == sid);
                if (seat != null && seat.IsAvailable)
                {
                    booking.AddSeat(sid); // додаємо місце до броні
                    seat.Reserve(); // позначаємо місце як зайняте
                }
            }

            booking.Recalculate(); // оновлюємо суму
            Bookings.Add(booking); // додаємо бронювання в список
            cust.AddBooking(booking.Id); // додаємо бронювання до клієнта
            return booking;
        }

        // Завершує покупку квитка після бронювання
        public Ticket PurchaseTicket(int bookingId, int customerId)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);
            var cust = Customers.FirstOrDefault(c => c.Id == customerId);
            if (booking == null || cust == null || booking.IsConfirmed) return null; // перевірка валідності

            // Створюємо новий платіж
            var pay = new Payment
            {
                Id = Payments.Count + 1,
                Amount = booking.TotalPrice,
                Method = "Wallet",
                CustomerId = customerId,
                Refundable = false
            };
            pay.Process(); // обробка платежу
            Payments.Add(pay);

            // Підтверджуємо бронювання
            var confirmed = booking.Confirm(pay.Id);
            if (!confirmed) return null;

            // Створюємо квитки для кожного місця
            foreach (var sid in booking.SeatIds)
            {
                var seat = ShowTimes.First(s => s.Id == booking.ShowTimeId).Hall.Seats.First(ss => ss.Id == sid);
                var t = new Ticket
                {
                    Id = Tickets.Count + 1,
                    ShowTimeId = booking.ShowTimeId,
                    Seat = seat,
                    Price = 100 * seat.PriceModifier, // розрахунок ціни з коефіцієнтом місця
                    PurchaserCustomerId = customerId
                };
                Tickets.Add(t);
                cust.AddTicket(t.Id); // додаємо квиток клієнту
            }

            // Повертаємо останній створений квиток
            return Tickets.LastOrDefault();
        }

        // Обчислює загальний потенційний прибуток кінотеатру (з усіх сеансів)
        public decimal CalculateTotalPotentialRevenue() => ShowTimes.Sum(s => s.CalculatePotentialRevenue());

        // Пошук сеансів за частиною назви фільму
        public IEnumerable<ShowTime> FindShowsByFilmTitle(string partial) =>
            ShowTimes.Where(s => s.Film.Title.Contains(partial, StringComparison.OrdinalIgnoreCase));

        // Додає нову знижку
        public void AddDiscount(Discount d) { if (!Discounts.Contains(d)) Discounts.Add(d); }

        // Додає новий відгук користувача
        public void AddReview(Review r) { Reviews.Add(r); }
    }
}
