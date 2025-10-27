using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    // Клас Genre представляє жанр фільму у системі кінотеатру
    public class Genre
    {
        // Унікальний ідентифікатор жанру
        public int Id { get; set; }

        // Назва жанру (наприклад: Drama, Action, Comedy)
        public string Name { get; set; }

        // Опис жанру
        public string Description { get; set; }

        // Популярність жанру (від 0 до 100)
        public int Popularity { get; private set; }

        // Мінімально дозволений вік для цього жанру
        public int MinAge { get; set; }

        // Чи активний жанр у системі
        public bool IsActive { get; private set; }

        // Дата створення запису жанру
        public DateTime CreatedAt { get; private set; }

        // Дата останнього оновлення
        public DateTime UpdatedAt { get; private set; }

        // Типова тривалість фільмів цього жанру
        public int TypicalDuration { get; set; }

        // Приклад фільму цього жанру
        public string ExampleFilm { get; set; }

        // Список Id фільмів, що належать до цього жанру
        public List<int> FilmIds { get; set; }

        // Популярність жанру в певному регіоні
        public string RegionPopularity { get; set; }

        // Конструктор для ініціалізації жанру
        public Genre(int id, string name, string description, int popularity, int minAge, bool isActive, DateTime createdAt, DateTime updatedAt, int typicalDuration, string exampleFilm)
        {
            Id = id;
            Name = name;
            Description = description;
            Popularity = popularity;
            MinAge = minAge;
            IsActive = isActive;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            TypicalDuration = typicalDuration;
            ExampleFilm = exampleFilm;
            FilmIds = new List<int>(); // список фільмів цього жанру
            RegionPopularity = "Global"; // за замовчуванням жанр глобально популярний
        }

        // Повертає коротку інформацію про жанр
        public string GetInfo()
        {
            return $"{Name} - {Description}. Popularity: {Popularity}/100, Min Age: {MinAge}+";
        }

        // Перевіряє чи підходить жанр для дитини певного віку
        public bool IsSuitableForChild(int age) => age >= MinAge;

        // Оновлює популярність жанру на певну величину в межах 0–100
        public void UpdatePopularity(int change)
        {
            Popularity = Math.Clamp(Popularity + change, 0, 100);
            UpdatedAt = DateTime.Now;
        }

        // Деактивує жанр (робить його недоступним)
        public void Deactivate() { IsActive = false; UpdatedAt = DateTime.Now; }

        // Оновлює опис жанру
        public void UpdateDescription(string newDescription) { Description = newDescription; UpdatedAt = DateTime.Now; }

        // Додає фільм до жанру
        public void AddFilm(int filmId) { if (!FilmIds.Contains(filmId)) FilmIds.Add(filmId); }

        // Видаляє фільм із жанру
        public void RemoveFilm(int filmId) { if (FilmIds.Contains(filmId)) FilmIds.Remove(filmId); }

        // Повертає типову тривалість фільму даного жанру
        public double AverageDurationEstimate() => TypicalDuration;

        // Встановлює популярність жанру для певного регіону (наприклад "EU", "USA")
        public void SetRegionPopularity(string region) { RegionPopularity = region; UpdatedAt = DateTime.Now; }
    }
}
