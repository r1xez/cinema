using System;
using System.Linq;
using System.Collections.Generic;

namespace cinema
{
    // Основна точка входу програми
    public class Program
    {
        static void Main(string[] args)
        {
            // Створення об'єкта кінотеатру
            var cinema = new Cinema { Name = "Atlas Cinema", Address = "Central St. 1" };

            // ================== Додавання жанрів ==================
            var g1 = new Genre(1, "Action", "High tempo action", 80, 12, true, DateTime.Now.AddYears(-5), DateTime.Now.AddYears(-1), 120, "Action Example");
            var g2 = new Genre(2, "Drama", "Emotional drama", 70, 16, true, DateTime.Now.AddYears(-7), DateTime.Now.AddYears(-2), 130, "Drama Example");
            cinema.AddGenre(g1);
            cinema.AddGenre(g2);

            // ================== Додавання фільмів ==================
            var f1 = new Film(1, "Fast Chase", "Action", g1.Description, 110, 8.2, 12, DateTime.Now.AddMonths(-1), "J. Speed", "USA", 1.2m);
            f1.AddCastMember("A. Star"); f1.AddCastMember("B. Side");
            var f2 = new Film(2, "Deep Feel", "Drama", g2.Description, 140, 7.5, 16, DateTime.Now.AddYears(-1), "L. Heart", "UK", 1.0m);
            f2.AddCastMember("C. Actor"); f2.AddCastMember("D. Actor");
            cinema.AddFilm(f1);
            cinema.AddFilm(f2);

            // ================== Додавання залів ==================
            var hall1 = new Hall { Id = 1, Name = "Hall 1", ScreenType = "LED", SoundSystem = "Dolby", Is3D = true, IsIMAX = false, OpenHours = "10:00-23:00" };
            hall1.InitializeSeats(5, 8, 4); // Створення сидінь у залі
            var hall2 = new Hall { Id = 2, Name = "Hall 2", ScreenType = "D-LED", SoundSystem = "DTS", Is3D = false, IsIMAX = true, OpenHours = "10:00-23:00" };
            hall2.InitializeSeats(6, 10, 5);
            cinema.AddHall(hall1);
            cinema.AddHall(hall2);

            // ================== Планування сеансів ==================
            var st1 = cinema.ScheduleShow(1, 1, DateTime.Today.AddHours(14), 120m, "EN", "UA");
            var st2 = cinema.ScheduleShow(2, 2, DateTime.Today.AddHours(16), 150m, "EN", "");
            Console.WriteLine($"Scheduled: {st1.Film.Title} in {st1.Hall.Name} at {st1.StartTime}");
            Console.WriteLine($"Scheduled: {st2.Film.Title} in {st2.Hall.Name} at {st2.StartTime}");

            // ================== Створення клієнта ==================
            var cust = new Customer { Id = 1, FirstName = "Ivan", LastName = "Petrov", BirthDate = DateTime.Now.AddYears(-25), Email = "ivan@example.com" };
            cust.AddFunds(500); // Додати кошти у гаманець
            cinema.Customers.Add(cust);

            // ================== Додавання персоналу ==================
            var staff = new Staff { Id = 1, FirstName = "Oksana", LastName = "Koval", EmployeeNumber = "EMP001", Salary = 15000m, HireDate = DateTime.Now.AddYears(-2) };
            staff.StartShift("Morning"); // Початок зміни
            staff.AssignTask("Check projector"); // Призначення завдання
            cinema.StaffMembers.Add(staff);

            // ================== Створення бронювання ==================
            var availableSeats = st1.GetAvailableSeats().Take(2).Select(s => s.Id).ToList(); // Беремо 2 доступних місця
            var booking = cinema.CreateBooking(cust.Id, st1.Id, availableSeats);
            Console.WriteLine("Booking created: " + booking.GetSummary());

            // ================== Купівля квитка ==================
            var ticket = cinema.PurchaseTicket(booking.Id, cust.Id);
            Console.WriteLine("Ticket purchased: ");
            Console.WriteLine(ticket?.PrintTicket() ?? "No ticket");

            // ================== Додавання відгуку ==================
            var review = new Review { Id = 1, CustomerId = cust.Id, FilmId = f1.Id, Rating = 9, Comment = "Very cool action!" };
            review.Approve(); // Затвердження відгуку
            cinema.AddReview(review);
            Console.WriteLine("Review added: " + review.Summarize());

            // ================== Лояльність клієнта ==================
            cust.ApplyLoyaltyPoints(200);
            Console.WriteLine($"Customer loyalty points: {cust.Loyalty?.Points}");

            // ================== Розрахунок потенційного доходу ==================
            Console.WriteLine($"Potential revenue today: {cinema.CalculateTotalPotentialRevenue():0.00}");

            // ================== Робота касира ==================
            var cashier = new Cashier { Id = 2, FirstName = "Pavlo", LastName = "Ivanov", EmployeeNumber = "C001", CanRefund = true };
            cashier.OpenTill(1000m); // Відкриття каси з початковою сумою
            cinema.StaffMembers.Add(cashier);

            var payment = new Payment { Id = 1, Amount = 200m, Method = "Card", CustomerId = cust.Id, Refundable = true };
            var processed = cashier.ProcessPayment(payment); // Обробка платежу
            Console.WriteLine($"Processed payment by cashier: {processed}, Transaction: {payment.TransactionId}");

            // ================== Завершення демо ==================
            Console.WriteLine("Demo finished. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
