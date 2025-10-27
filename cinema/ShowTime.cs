using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    // Клас, що описує конкретний показ фільму в залі
    public class ShowTime
    {
        public int Id { get; set; }                   // Унікальний ідентифікатор показу
        public Film Film { get; set; }                // Фільм, що показується
        public Hall Hall { get; set; }                // Зала, де проходить показ
        public DateTime StartTime { get; set; }       // Час початку показу
        public DateTime EndTime { get; set; }         // Час закінчення показу
        public int AvailableSeats => Hall?.Seats.Count(s => s.IsAvailable) ?? 0; // Кількість доступних місць
        public decimal BasePrice { get; set; }        // Базова ціна квитка
        public string Language { get; set; }          // Мова фільму
        public string Subtitle { get; set; }          // Субтитри
        public bool IsSoldOut => AvailableSeats == 0; // Показ розпроданий?
        public List<int> BookingIds { get; set; }     // Список ідентифікаторів бронювань
        public bool Is3DSession { get; set; }         // Показ у форматі 3D

        // Конструктор
        public ShowTime()
        {
            BookingIds = new List<int>();
        }

        // Резервування місця на показ
        public bool BookSeat(Seat seat)
        {
            if (seat == null || !seat.IsAvailable) return false;
            return seat.Reserve();
        }

        // Скасування броні місця
        public void CancelSeat(Seat seat) { seat?.Release(); }

        // Повертає кількість доступних місць
        public int GetRemainingSeats() => AvailableSeats;

        // Продовження тривалості показу
        public void ExtendShow(TimeSpan extra) { EndTime = EndTime.Add(extra); }

        // Зміна базової ціни квитка
        public void ChangePrice(decimal newPrice) { BasePrice = newPrice; }

        // Розрахунок потенційного доходу від всіх місць з урахуванням їхніх PriceModifier
        public decimal CalculatePotentialRevenue()
        {
            return Hall.Seats.Sum(s => (decimal)s.PriceModifier * BasePrice);
        }

        // Повертає список доступних для броні місць
        public IEnumerable<Seat> GetAvailableSeats() => Hall?.Seats.Where(s => s.IsAvailable) ?? new List<Seat>();
    }
}
