using System;
using System.Collections.Generic;

namespace cinema
{
    // Клас LoyaltyCard представляє картку лояльності клієнта
    public class LoyaltyCard
    {
        // Унікальний номер картки
        public string CardNumber { get; set; }

        // Поточні бали на картці
        public int Points { get; private set; }

        // Рівень картки (Bronze, Silver, Gold)
        public string Tier { get; private set; }

        // Id власника картки (Customer)
        public int OwnerCustomerId { get; set; }

        // Дата видачі картки
        public DateTime IssuedAt { get; set; }

        // Дата закінчення дії картки
        public DateTime ExpiryDate { get; set; }

        // Чи активна картка
        public bool IsActive { get; set; }

        // Множник бонусів (для нарахування додаткових балів)
        public decimal BonusMultiplier { get; set; }

        // Історія використання та нарахування балів
        public List<string> RedemptionHistory { get; set; }

        // Максимальна кількість балів на картці
        public int MaxPoints { get; set; }

        // Ім'я власника картки
        public string HolderName { get; set; }

        // Конструктор за замовчуванням
        public LoyaltyCard()
        {
            RedemptionHistory = new List<string>();
            IsActive = true;
            IssuedAt = DateTime.Now;
            ExpiryDate = IssuedAt.AddYears(3); // діє 3 роки
            MaxPoints = 100000;
            BonusMultiplier = 1.0m;
            CardNumber = "LC-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        // Конструктор для прив'язки картки до конкретного клієнта
        public LoyaltyCard(Customer owner) : this()
        {
            OwnerCustomerId = owner.Id;
            HolderName = owner.FullName;
        }

        // Нарахування балів на картку
        public void AddPoints(int pts)
        {
            Points = Math.Min(MaxPoints, Points + (int)(pts * BonusMultiplier));
            UpdateTier(); // оновлення рівня картки залежно від балів
            RedemptionHistory.Add($"Added {pts} pts at {DateTime.Now}");
        }

        // Використання (списання) балів
        public bool RedeemPoints(int pts)
        {
            if (Points >= pts)
            {
                Points -= pts;
                RedemptionHistory.Add($"Redeemed {pts} pts at {DateTime.Now}");
                UpdateTier(); // оновлення рівня картки
                return true;
            }
            return false;
        }

        // Примусове оновлення рівня картки
        public void UpgradeTier(string newTier)
        {
            Tier = newTier;
            RedemptionHistory.Add($"Upgraded to {newTier}");
        }

        // Закінчення дії картки
        public void Expire() { IsActive = false; }

        // Передача балів іншій активній картці
        public bool TransferPoints(LoyaltyCard other, int pts)
        {
            if (Points < pts || !other.IsActive) return false;
            Points -= pts;
            other.AddPoints(pts);
            return true;
        }

        // Приватний метод для оновлення рівня картки залежно від балів
        private void UpdateTier()
        {
            if (Points > 5000) Tier = "Gold";
            else if (Points > 1000) Tier = "Silver";
            else Tier = "Bronze";
        }
    }
}
