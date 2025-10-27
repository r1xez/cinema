using System;
using System.Collections.Generic;

namespace cinema
{
    // Клас Discount представляє знижку, яку можна застосувати до покупки квитків
    public class Discount
    {
        // Унікальний ідентифікатор знижки
        public int Id { get; set; }

        // Назва знижки (наприклад: "Student Discount", "Black Friday")
        public string Name { get; set; }

        // Відсоткова знижка (наприклад 10% або 25%)
        public int Percentage { get; set; }

        // Фіксована знижка у грошах (наприклад -50 грн)
        public decimal FixedAmount { get; set; }

        // Дата початку дії знижки
        public DateTime ValidFrom { get; set; }

        // Дата завершення дії знижки
        public DateTime ValidTo { get; set; }

        // Список жанрів фільмів, до яких застосовується знижка (якщо пустий — діє на всі)
        public List<string> ApplicableGenres { get; set; }

        // Мінімальна сума покупки, щоб знижка діяла
        public decimal MinPurchaseAmount { get; set; }

        // Максимальна кількість використань знижки одним користувачем
        public int MaxUsagePerCustomer { get; set; }

        // Активна знижка чи вимкнена
        public bool IsActive { get; set; }

        // Промокод для цієї знижки (наприклад "SALE50")
        public string Code { get; set; }

        // Конструктор — встановлює значення за замовчуванням
        public Discount()
        {
            ApplicableGenres = new List<string>();
            IsActive = true; // знижка активна при створенні
        }

        // Застосувати знижку до суми
        public decimal ApplyDiscount(decimal amount)
        {
            // Якщо знижка недійсна або сума замала — повернути початкову суму
            if (!IsValid() || amount < MinPurchaseAmount) return amount;

            // Обчислити знижку у відсотках
            decimal byPercent = amount - (amount * Percentage / 100m);

            // Обчислити знижку у фіксованій сумі
            decimal byFixed = amount - FixedAmount;

            // Вибрати найменшу з цих сум (максимальна вигода для клієнта)
            var result = Math.Min(byPercent, byFixed);

            // Не допускаємо від’ємної суми
            return Math.Max(0, result);
        }

        // Перевіряє чи діє знижка на даний момент
        public bool IsValid()
        {
            var now = DateTime.Now;
            return IsActive && now >= ValidFrom && now <= ValidTo;
        }

        // Активувати знижку
        public void Activate() { IsActive = true; }

        // Деактивувати знижку
        public void Deactivate() { IsActive = false; }

        // Перевірити чи можна використати знижку у певному випадку
        public bool CanUse(string genre, decimal amount)
        {
            // Якщо знижка активна, жанр підходить і сума достатня — можна використати
            return IsValid() &&
                   (ApplicableGenres.Count == 0 || ApplicableGenres.Contains(genre)) &&
                   amount >= MinPurchaseAmount;
        }

        // Додати жанр до списку дозволених
        public void AddGenre(string genre) { if (!ApplicableGenres.Contains(genre)) ApplicableGenres.Add(genre); }

        // Видалити жанр зі списку дозволених
        public void RemoveGenre(string genre) { if (ApplicableGenres.Contains(genre)) ApplicableGenres.Remove(genre); }
    }
}
