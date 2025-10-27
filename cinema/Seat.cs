using System;

namespace cinema
{
    // Клас, що описує місце в залі кінотеатру
    public class Seat
    {
        public string Id { get; set; }               // Унікальний ідентифікатор місця
        public int Row { get; set; }                 // Ряд місця в залі
        public int Number { get; set; }              // Номер місця в ряді
        public bool IsAvailable { get; set; }        // Статус доступності місця
        public string SeatType { get; set; }         // Тип місця (наприклад, "normal", "premium", "broken")
        public int ComfortLevel { get; set; }        // Рівень комфорту місця (оцінка від 1 до 10)
        public decimal PriceModifier { get; set; }   // Множник ціни квитка для цього місця
        public DateTime LastCleaned { get; set; }    // Дата останнього прибирання місця
        public string LocationDescription { get; set; } // Текстовий опис розташування (наприклад, "Row 3 Seat 5")
        public int HallId { get; set; }              // Ідентифікатор зали, до якої належить місце

        // Конструктор: за замовчуванням місце доступне та згенерований унікальний Id
        public Seat()
        {
            IsAvailable = true;
            LastCleaned = DateTime.Now.AddDays(-1); // Припускаємо, що місце було прибране вчора
            Id = Guid.NewGuid().ToString();
        }

        // Метод резервування місця
        public bool Reserve()
        {
            if (!IsAvailable) return false;
            IsAvailable = false;
            return true;
        }

        // Метод звільнення місця (робить доступним)
        public void Release() { IsAvailable = true; }

        // Позначення місця як пошкодженого, робить його недоступним
        public void MarkAsBroken() { SeatType = "broken"; IsAvailable = false; }

        // Оновлення дати прибирання
        public void Clean() { LastCleaned = DateTime.Now; }

        // Отримання інформації про місце у зручному вигляді
        public string GetInfo() => $"Seat {Row}-{Number} [{SeatType}] Available: {IsAvailable}";
    }
}
