using System;
using System.Collections.Generic;

namespace cinema
{
    // Клас Customer наслідує Person і представляє клієнта кінотеатру
    public class Customer : Person
    {
        // Унікальний номер клієнта
        public string CustomerNumber { get; set; }

        // Карта лояльності клієнта (може бути відсутня)
        public LoyaltyCard Loyalty { get; set; }

        // Список ID бронювань, які належать клієнту
        public List<int> BookingIds { get; set; }

        // Баланс на внутрішньому гаманці клієнта
        public decimal WalletBalance { get; set; }

        // Улюблені жанри фільмів
        public List<string> PreferredGenres { get; set; }

        // Список ID куплених квитків
        public List<int> TicketIds { get; set; }

        // Стан підтвердження електронної пошти
        public bool EmailVerified { get; set; }

        // Дата реєстрації клієнта
        public DateTime RegisteredAt { get; set; }

        // Загальна сума витрачених коштів
        public decimal TotalSpent { get; set; }

        // Чи заблокований клієнт
        public bool IsBlocked { get; set; }

        // Конструктор: встановлює роль, ініціалізує списки і реєстраційну дату
        public Customer()
        {
            Role = "Customer"; // Властивість із базового класу Person
            BookingIds = new List<int>();
            PreferredGenres = new List<string>();
            TicketIds = new List<int>();
            RegisteredAt = DateTime.Now; // Поточна дата при створенні
        }

        // Додає нове бронювання клієнту, якщо такого ще немає
        public void AddBooking(int bookingId) { if (!BookingIds.Contains(bookingId)) BookingIds.Add(bookingId); }

        // Скасовує бронювання за ID
        public void CancelBooking(int bookingId) { if (BookingIds.Contains(bookingId)) BookingIds.Remove(bookingId); }

        // Поповнює баланс гаманця
        public void AddFunds(decimal amount) { WalletBalance += amount; }

        // Списує кошти з гаманця, якщо достатньо балансу
        public bool DeductFunds(decimal amount)
        {
            if (WalletBalance >= amount) { WalletBalance -= amount; TotalSpent += amount; return true; }
            return false;
        }

        // Додає квиток у список, якщо такого ще немає
        public void AddTicket(int ticketId) { if (!TicketIds.Contains(ticketId)) TicketIds.Add(ticketId); }

        // Перевіряє, чи клієнт повнолітній (використовуючи метод GetAge() з Person)
        public bool IsAdult() => GetAge() >= 18;

        // Додає бонусні бали за картою лояльності
        public void ApplyLoyaltyPoints(int points)
        {
            Loyalty ??= new LoyaltyCard(this); // Створює карту, якщо її ще немає
            Loyalty.AddPoints(points);
        }

        // Блокує клієнта (наприклад, за порушення)
        public void BlockCustomer() { IsBlocked = true; }

        // Розблоковує клієнта
        public void UnblockCustomer() { IsBlocked = false; }

        // Додає улюблений жанр, якщо він ще не доданий
        public void AddPreferredGenre(string genre) { if (!PreferredGenres.Contains(genre)) PreferredGenres.Add(genre); }
    }
}
