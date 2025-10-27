using System;
using System.Collections.Generic;

namespace cinema
{
    // Клас Booking представляє бронювання квитків у кіно
    public class Booking
    {
        // Унікальний ID бронювання
        public int Id { get; set; }

        // ID клієнта, який зробив бронювання
        public int CustomerId { get; set; }

        // ID сеансу, на який зроблено бронювання
        public int ShowTimeId { get; set; }

        // Список місць, які заброньовані (наприклад "A1", "B3")
        public List<string> SeatIds { get; set; }

        // Поточний статус бронювання ("Created", "Confirmed", "Expired")
        public string Status { get; set; }

        // Дата та час створення бронювання
        public DateTime CreatedAt { get; set; }

        // Час, до якого бронювання дійсне (запас часу для оплати)
        public DateTime ExpiresAt { get; set; }

        // Загальна вартість бронювання
        public decimal TotalPrice { get; set; }

        // ID платежу, якщо бронювання оплачено
        public int PaymentId { get; set; }

        // Чи підтверджено бронювання
        public bool IsConfirmed { get; set; }

        // Унікальний код бронювання для користувача
        public string BookingReference { get; set; }

        // Дата та час підтвердження бронювання (може бути null)
        public DateTime? ConfirmedAt { get; set; }

        // Конструктор - встановлює початкові значення при створенні бронювання
        public Booking()
        {
            SeatIds = new List<string>(); // Ініціалізація списку місць
            CreatedAt = DateTime.Now; // Час створення бронювання
            ExpiresAt = CreatedAt.AddMinutes(15); // Час, коли бронь стане неактивною
            Status = "Created"; // Початковий статус
            BookingReference = "BKG-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(); // Генерація коду бронювання
        }

        // Додає місце до бронювання, якщо воно ще не додане
        public void AddSeat(string seatId) { if (!SeatIds.Contains(seatId)) SeatIds.Add(seatId); Recalculate(); }

        // Видаляє місце з бронювання
        public void RemoveSeat(string seatId) { if (SeatIds.Contains(seatId)) SeatIds.Remove(seatId); Recalculate(); }

        // Оновлює загальну вартість броні (ціна за 1 місце = 100)
        public void Recalculate()
        {
            TotalPrice = SeatIds.Count * 100m;
        }

        // Підтверджує бронювання, якщо воно ще активне і не прострочене
        public bool Confirm(int paymentId)
        {
            if (Status == "Created" && DateTime.Now <= ExpiresAt)
            {
                IsConfirmed = true;
                Status = "Confirmed"; // Статус змінюється на підтверджене
                PaymentId = paymentId; // Присвоюємо ID платежу
                ConfirmedAt = DateTime.Now; // Час підтвердження
                return true;
            }
            return false;
        }

        // Примусово переводить бронювання у статус "Expired"
        public void Expire() { Status = "Expired"; }

        // Повертає короткий опис бронювання
        public string GetSummary() => $"{BookingReference} | Seats: {SeatIds.Count} | Total: {TotalPrice:0.00} | Status: {Status}";

        // Перевіряє, чи бронювання прострочено
        public bool IsExpired() => DateTime.Now > ExpiresAt;
    }
}
