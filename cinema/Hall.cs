using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Seat> Seats { get; set; }
        public string ScreenType { get; set; }
        public string SoundSystem { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public bool Is3D { get; set; }
        public bool IsIMAX { get; set; }
        public string OpenHours { get; set; }

        public Hall()
        {
            Seats = new List<Seat>();
        }

        public void InitializeSeats(int rows, int cols, decimal premiumEveryNth = 5)
        {
            Rows = rows; Columns = cols;
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
            Capacity = Seats.Count;
        }

        public Seat FindAvailableSeat()
        {
            return Seats.FirstOrDefault(s => s.IsAvailable && s.SeatType != "broken");
        }

        public bool ReserveSeat(string seatId)
        {
            var seat = Seats.FirstOrDefault(s => s.Id == seatId);
            if (seat == null) return false;
            return seat.Reserve();
        }

        public void ReleaseSeat(string seatId)
        {
            var seat = Seats.FirstOrDefault(s => s.Id == seatId);
            seat?.Release();
        }

        public double GetOccupancyRate()
        {
            if (!Seats.Any()) return 0;
            var occupied = Seats.Count(s => !s.IsAvailable);
            return (double)occupied / Seats.Count;
        }

        public IEnumerable<Seat> GetAvailableSeats() => Seats.Where(s => s.IsAvailable);
    }
}
