using System;
using System.Collections.Generic;

namespace cinema
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Percentage { get; set; }
        public decimal FixedAmount { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public List<string> ApplicableGenres { get; set; }
        public decimal MinPurchaseAmount { get; set; }
        public int MaxUsagePerCustomer { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }

        public Discount()
        {
            ApplicableGenres = new List<string>();
            IsActive = true;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            if (!IsValid() || amount < MinPurchaseAmount) return amount;
            decimal byPercent = amount - (amount * Percentage / 100m);
            decimal byFixed = amount - FixedAmount;
            var result = Math.Min(byPercent, byFixed);
            return Math.Max(0, result);
        }

        public bool IsValid()
        {
            var now = DateTime.Now;
            return IsActive && now >= ValidFrom && now <= ValidTo;
        }

        public void Activate() { IsActive = true; }

        public void Deactivate() { IsActive = false; }

        public bool CanUse(string genre, decimal amount)
        {
            return IsValid() && (ApplicableGenres.Count == 0 || ApplicableGenres.Contains(genre)) && amount >= MinPurchaseAmount;
        }

        public void AddGenre(string genre) { if (!ApplicableGenres.Contains(genre)) ApplicableGenres.Add(genre); }

        public void RemoveGenre(string genre) { if (ApplicableGenres.Contains(genre)) ApplicableGenres.Remove(genre); }
    }
}
