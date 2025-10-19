using System;

namespace cinema
{
    public class Seat
    {
        public string Id { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
        public string SeatType { get; set; }
        public int ComfortLevel { get; set; }
        public decimal PriceModifier { get; set; }
        public DateTime LastCleaned { get; set; }
        public string LocationDescription { get; set; }
        public int HallId { get; set; }

        public Seat()
        {
            IsAvailable = true;
            LastCleaned = DateTime.Now.AddDays(-1);
            Id = Guid.NewGuid().ToString();
        }

        public bool Reserve()
        {
            if (!IsAvailable) return false;
            IsAvailable = false;
            return true;
        }

        public void Release() { IsAvailable = true; }

        public void MarkAsBroken() { SeatType = "broken"; IsAvailable = false; }

        public void Clean() { LastCleaned = DateTime.Now; }

        public string GetInfo() => $"Seat {Row}-{Number} [{SeatType}] Available: {IsAvailable}";
    }
}
