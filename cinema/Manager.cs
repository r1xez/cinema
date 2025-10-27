using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    // Клас Manager представляє менеджера кінотеатру
    // Наслідує Staff, тому має базові властивості співробітника
    public class Manager : Staff
    {
        // Список Id співробітників, якими керує менеджер
        public List<int> ManagedStaffIds { get; set; }

        // Бюджет, яким керує менеджер
        public decimal Budget { get; set; }

        // Стаж роботи на позиції менеджера (роки)
        public int YearsInPosition { get; set; }

        // Номер офісу менеджера
        public string OfficeNumber { get; set; }

        // Звіти та записи про управлінські дії
        public List<string> Reports { get; set; }

        // Чи може менеджер приймати на роботу нових співробітників
        public bool CanHire { get; set; }

        // Чи може менеджер звільняти співробітників
        public bool CanFire { get; set; }

        // Максимальна кількість співробітників, якими може керувати менеджер
        public int MaxStaffManaged { get; set; }

        // Регіон відповідальності менеджера
        public string Region { get; set; }

        // Дата підвищення до цієї позиції
        public DateTime PromotedAt { get; set; }

        // Конструктор — ініціалізує списки та роль
        public Manager()
        {
            Role = "Manager";
            ManagedStaffIds = new List<int>();
            Reports = new List<string>();
        }

        // Прийом співробітника на роботу
        public void HireStaff(Staff s)
        {
            if (s == null) return;
            ManagedStaffIds.Add(s.Id);
            Reports.Add($"Hired {s.FullName} at {DateTime.Now}");
        }

        // Звільнення співробітника
        public void FireStaff(Staff s)
        {
            if (s == null) return;
            ManagedStaffIds.Remove(s.Id);
            Reports.Add($"Fired {s.FullName} at {DateTime.Now}");
        }

        // Виділення коштів з бюджету
        public void AllocateBudget(decimal amount) { Budget -= amount; }

        // Додавання власного звіту
        public void AddReport(string report) { Reports.Add(report); }

        // Отримання списку всіх звітів
        public IEnumerable<string> GetReports() => Reports.AsEnumerable();
    }
}
