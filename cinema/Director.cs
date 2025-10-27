using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    // Клас Director наслідує Customer, а отже також є користувачем системи,
    // але з додатковими полями, що описують режисера кіно
    public class Director : Customer
    {
        // Список ID фільмів, які зняв цей режисер
        public List<int> DirectedFilmIds { get; set; }

        // Перелік нагород, які отримав режисер
        public List<string> Awards { get; set; }

        // Кінематографічний стиль (наприклад: "Неореалізм", "Сюрреалізм")
        public string Style { get; set; }

        // Середній бюджет фільмів режисера
        public decimal AvgBudget { get; set; }

        // Контакти агента режисера
        public string AgentContact { get; set; }

        // Чи працює режисер зараз у кіноіндустрії
        public bool IsActiveDirector { get; set; }

        // Скільки років режисер працює у сфері
        public int YearsActive { get; set; }

        // Перелік найвідоміших робіт режисера
        public List<string> NotableWorks { get; set; }

        // Кількість нагород
        public int AwardsCount { get; set; }

        // Рейтинг режисера (може використовуватись як популярність чи оцінка)
        public double Rating { get; set; }

        // Конструктор класу - встановлює роль та ініціалізує списки
        public Director()
        {
            Role = "Director"; // Перезаписуємо роль користувача (оскільки наслідуємо Customer)
            DirectedFilmIds = new List<int>();
            Awards = new List<string>();
            NotableWorks = new List<string>();
        }

        // Додає ID фільму у список знятих фільмів
        public void AddDirectedFilm(int filmId)
        {
            if (!DirectedFilmIds.Contains(filmId))
                DirectedFilmIds.Add(filmId);
        }

        // Видаляє ID фільму зі списку знятих
        public void RemoveDirectedFilm(int filmId)
        {
            if (DirectedFilmIds.Contains(filmId))
                DirectedFilmIds.Remove(filmId);
        }

        // Повертає кількість фільмів, які зняв режисер
        public int GetFilmCount() => DirectedFilmIds.Count;

        // Обчислює середню тривалість фільмів режисера
        public decimal CalculateAvgFilmDuration(List<Film> films)
        {
            // Знаходимо фільми, які відповідають ID режисера
            var directed = films.Where(f => DirectedFilmIds.Contains(f.Id)).ToList();

            if (!directed.Any())
                return 0;

            // Обчислюємо середню тривалість
            return (decimal)directed.Average(f => f.DurationMinutes);
        }

        // Додає нову нагороду режисеру та збільшує рейтинг
        public void AwardPrize(string prize)
        {
            Awards.Add(prize);    // Додаємо нагороду
            AwardsCount++;        // Лічильник нагород
            Rating += 0.5;        // Рейтинг зростає
        }

        // Перевизначення методу з базового класу для короткого опису
        public override string GetSummary() => $"{FullName} - Director, Films: {GetFilmCount()}, Rating: {Rating}";

        // Встановлює активність режисера (працює чи ні)
        public void SetActive(bool active)
        {
            IsActiveDirector = active;
        }

        // Додає відому роботу в список
        public void AddNotableWork(string work)
        {
            if (!NotableWorks.Contains(work))
                NotableWorks.Add(work);
        }
    }
}
