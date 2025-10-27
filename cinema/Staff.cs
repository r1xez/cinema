using System;
using System.Collections.Generic;

namespace cinema
{
    // Клас, що описує працівника кінотеатру
    public class Staff : Person
    {
        public string Position { get; set; }          // Посада працівника
        public decimal Salary { get; set; }           // Зарплата
        public string EmployeeNumber { get; set; }    // Унікальний номер співробітника
        public DateTime HireDate { get; set; }        // Дата найму
        public bool IsOnShift { get; set; }           // Чи на даний момент співробітник на зміні
        public List<string> Permissions { get; set; } // Список дозволів/повноважень
        public int ManagedHallId { get; set; }        // Зала, якою керує (якщо є)
        public List<string> Shifts { get; set; }      // Історія змін (початок/кінець)
        public int SupervisorId { get; set; }         // Ідентифікатор керівника
        public List<string> Tasks { get; set; }       // Поточні/призначені завдання

        // Конструктор
        public Staff()
        {
            Role = "Staff";                           // Тип ролі
            Permissions = new List<string>();
            Shifts = new List<string>();
            Tasks = new List<string>();
        }

        // Почати зміну
        public void StartShift(string shift)
        {
            IsOnShift = true;
            Shifts.Add(shift);
        }

        // Закінчити зміну
        public void EndShift()
        {
            IsOnShift = false;
            Shifts.Add("EndedShift@" + DateTime.Now.ToString("s"));
        }

        // Призначити завдання працівнику
        public void AssignTask(string task) { Tasks.Add(task); }

        // Виконати завдання (видалити з поточних)
        public void CompleteTask(string task) { if (Tasks.Contains(task)) Tasks.Remove(task); }

        // Підвищити працівника, змінити зарплату і додати роль "Manager"
        public void Promote(decimal newSalary)
        {
            Salary = newSalary;
            if (!Permissions.Contains("Manager")) Permissions.Add("Manager");
        }

        // Повертає статус: на зміні чи ні
        public string Status() => IsOnShift ? "OnShift" : "OffShift";

        // Додати новий дозвіл/право
        public void AddPermission(string perm) { if (!Permissions.Contains(perm)) Permissions.Add(perm); }
    }
}
