using System;
using System.Collections.Generic;

namespace cinema
{
    public class LoyaltyCard
    {
        public string CardNumber { get; set; }
        public int Points { get; private set; }
        public string Tier { get; private set; }
        public int OwnerCustomerId { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public decimal BonusMultiplier { get; set; }
        public List<string> RedemptionHistory { get; set; }
        public int MaxPoints { get; set; }
        public string HolderName { get; set; }

        public LoyaltyCard() { RedemptionHistory = new List<string>(); IsActive = true; IssuedAt = DateTime.Now; ExpiryDate = IssuedAt.AddYears(3); MaxPoints = 100000; BonusMultiplier = 1.0m; CardNumber = "LC-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(); }

        public LoyaltyCard(Customer owner) : this()
        {
            OwnerCustomerId = owner.Id;
            HolderName = owner.FullName;
        }

        public void AddPoints(int pts)
        {
            Points = Math.Min(MaxPoints, Points + (int)(pts * BonusMultiplier));
            UpdateTier();
            RedemptionHistory.Add($"Added {pts} pts at {DateTime.Now}");
        }

        public bool RedeemPoints(int pts)
        {
            if (Points >= pts)
            {
                Points -= pts;
                RedemptionHistory.Add($"Redeemed {pts} pts at {DateTime.Now}");
                UpdateTier();
                return true;
            }
            return false;
        }

        public void UpgradeTier(string newTier) { Tier = newTier; RedemptionHistory.Add($"Upgraded to {newTier}"); }

        public void Expire() { IsActive = false; }

        public bool TransferPoints(LoyaltyCard other, int pts)
        {
            if (Points < pts || !other.IsActive) return false;
            Points -= pts;
            other.AddPoints(pts);
            return true;
        }

        private void UpdateTier()
        {
            if (Points > 5000) Tier = "Gold";
            else if (Points > 1000) Tier = "Silver";
            else Tier = "Bronze";
        }
    }
}
