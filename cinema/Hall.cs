using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    // Клас Hall представляє кінозал у кінотеатрі
    public class Hall
    {
        // Унікальний ідентифікатор залу
        public int Id { get; set; }

        // Назва залу (наприклад: "Main Hall", "VIP Hall")
        public string Name { get; set; }

        // Загальна місткість залу (кількість місць)
        public int Capacity { get; set; }

        // Список всіх місць (Seat) у залі
        public List<Seat> Seats { get; set; }

        // Тип екрану (наприклад: "LCD", "LED", "IMAX")
        public string ScreenType { get; set; }

        // Тип звукової системи
        public string SoundSystem { get; set; }

        // Кількість рядів
        public int Rows { get; set; }

        // Кількість колонок
        public int Columns { get; set; }

        // Чи підтримує 3D
        public bool Is3D { get; set; }

        // Чи є IMAX
        public bool IsIMAX { get; set; }

        // Години роботи залу
        public string OpenHours { get; set; }

        // Конструктор — ініціалізує список місць
        public Hall()
        {
            Seats = new List<Seat>();
        }

        // Ініціалізація місць у залі
        // rows, cols — розташування рядів і колонок
        // premiumEveryNth — кожне n-те місце буде преміум класу
        public void InitializeSeats(int rows, int cols, decimal premiumEveryNth = 5)
        {
            Rows = rows;
            Columns = cols;
            Seats.Clear();

            for (int r = 1; r <= rows; r++)
            {
                for (int c = 1; c <= cols; c++)
                {
                    var seat = new Seat
                    {
                        Row = r,
                        Number = c,
                        HallId = Id,
                        SeatType = ((c % (int)premiumEveryNth == 0) ? "premium" : "normal"),
                        ComfortLevel = (c % (int)premiumEveryNth == 0) ? 8 : 5,
                        PriceModifier = (c % (int)premiumEveryNth == 0) ? 1.5m : 1.0m,
                        LocationDescription = $"Row {r} Seat {c}"
                    };
                    Seats.Add(seat);
                }
            }
            Capacity = Seats.Count; // оновлюємо загальну місткість залу
        }

        // Пошук доступного місця
        public Seat FindAvailableSeat()
        {
            return Seats.FirstOrDefault(s => s.IsAvailable && s.SeatType != "broken");
        }

        // Резервування місця за його Id
        public bool ReserveSeat(string seatId)
        {
            var seat = Seats.FirstOrDefault(s => s.Id == seatId);
            if (seat == null) return false;
            return seat.Reserve();
        }

        // Звільнення місця за його Id
        public void ReleaseSeat(string seatId)
        {
            var seat = Seats.FirstOrDefault(s => s.Id == seatId);
            seat?.Release();
        }

        // Обчислення заповнюваності залу (від 0 до 1)
        public double GetOccupancyRate()
        {
            if (!Seats.Any()) return 0;
            var occupied = Seats.Count(s => !s.IsAvailable);
            return (double)occupied / Seats.Count;
        }

        // Повертає всі доступні місця
        public IEnumerable<Seat> GetAvailableSeats() => Seats.Where(s => s.IsAvailable);
    }
}
