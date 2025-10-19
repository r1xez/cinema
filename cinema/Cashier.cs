using System;

namespace cinema
{
    public class Cashier : Staff
    {
        public int TransactionsHandled { get; set; }
        public decimal CashInDrawer { get; set; }
        public string TerminalId { get; set; }
        public bool CanRefund { get; set; }
        public int ShiftCounter { get; set; }
        public string TillNumber { get; set; }
        public int SalesToday { get; set; }
        public bool IsLoggedIn { get; set; }
        public string BadgeId { get; set; }
        public decimal DailyTarget { get; set; }

        public Cashier() { Role = "Cashier"; }

        public void OpenTill(decimal startingCash) { CashInDrawer = startingCash; IsLoggedIn = true; ShiftCounter++; }

        public void CloseTill() { IsLoggedIn = false; CashInDrawer = 0; }

        public bool ProcessPayment(Payment p)
        {
            if (p == null) return false;
            if (p.Process()) { TransactionsHandled++; CashInDrawer += p.Amount - p.CalculateFee(); SalesToday++; return true; }
            return false;
        }

        public bool RefundPayment(Payment p)
        {
            if (!CanRefund || p == null) return false;
            if (p.Refund()) { CashInDrawer -= p.Amount; return true; }
            return false;
        }

        public void IncrementSales(int count) { SalesToday += count; }
    }
}
