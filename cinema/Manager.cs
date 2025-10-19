using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    public class Manager : Staff
    {
        public List<int> ManagedStaffIds { get; set; }
        public decimal Budget { get; set; }
        public int YearsInPosition { get; set; }
        public string OfficeNumber { get; set; }
        public List<string> Reports { get; set; }
        public bool CanHire { get; set; }
        public bool CanFire { get; set; }
        public int MaxStaffManaged { get; set; }
        public string Region { get; set; }
        public DateTime PromotedAt { get; set; }

        public Manager()
        {
            Role = "Manager";
            ManagedStaffIds = new List<int>();
            Reports = new List<string>();
        }

        public void HireStaff(Staff s)
        {
            if (s == null) return;
            ManagedStaffIds.Add(s.Id);
            Reports.Add($"Hired {s.FullName} at {DateTime.Now}");
        }

        public void FireStaff(Staff s)
        {
            if (s == null) return;
            ManagedStaffIds.Remove(s.Id);
            Reports.Add($"Fired {s.FullName} at {DateTime.Now}");
        }

        public void AllocateBudget(decimal amount) { Budget -= amount; }

        public void AddReport(string report) { Reports.Add(report); }

        public IEnumerable<string> GetReports() => Reports.AsEnumerable();
    }
}
