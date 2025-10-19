using System;
using System.Collections.Generic;

namespace cinema
{
    public class Staff : Person
    {
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsOnShift { get; set; }
        public List<string> Permissions { get; set; }
        public int ManagedHallId { get; set; }
        public List<string> Shifts { get; set; }
        public int SupervisorId { get; set; }
        public List<string> Tasks { get; set; }

        public Staff()
        {
            Role = "Staff";
            Permissions = new List<string>();
            Shifts = new List<string>();
            Tasks = new List<string>();
        }

        public void StartShift(string shift)
        {
            IsOnShift = true;
            Shifts.Add(shift);
        }

        public void EndShift()
        {
            IsOnShift = false;
            Shifts.Add("EndedShift@" + DateTime.Now.ToString("s"));
        }

        public void AssignTask(string task) { Tasks.Add(task); }

        public void CompleteTask(string task) { if (Tasks.Contains(task)) Tasks.Remove(task); }

        public void Promote(decimal newSalary)
        {
            Salary = newSalary;
            if (!Permissions.Contains("Manager")) Permissions.Add("Manager");
        }

        public string Status() => IsOnShift ? "OnShift" : "OffShift";

        public void AddPermission(string perm) { if (!Permissions.Contains(perm)) Permissions.Add(perm); }
    }
}
