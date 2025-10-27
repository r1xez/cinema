using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    // Клас Actor успадковує властивості та методи від класу Customer
    public class Actor : Customer
    {
        // Список ID фільмів, у яких знімався актор
        public List<int> FilmographyIds { get; set; }

        // Список нагород актора
        public List<string> Awards { get; set; }

        // Ім'я або компанія агента актора
        public string Agent { get; set; }

        // Зріст актора в сантиметрах
        public decimal HeightCm { get; set; }

        // Вага актора в кілограмах
        public decimal WeightKg { get; set; }

        // Чи доступний актор для зйомок
        public bool IsAvailable { get; set; }

        // Популярність актора (якийсь числовий рейтинг)
        public double Popularity { get; set; }

        // Рік дебюту актора в кіно
        public int DebutYear { get; set; }

        // Мови, якими володіє актор
        public List<string> Languages { get; set; }

        // Кількість виступів/ролей за рік
        public int AnnualShows { get; set; }

        // Конструктор класу
        public Actor()
        {
            Role = "Actor"; // Поле Role успадковане від Customer
            FilmographyIds = new List<int>(); // Ініціалізація списку фільмів
            Awards = new List<string>(); // Ініціалізація списку нагород
            Languages = new List<string>(); // Ініціалізація мов
        }

        // Додає фільм у фільмографію, якщо його там ще немає
        public void AddFilmography(int filmId) { if (!FilmographyIds.Contains(filmId)) FilmographyIds.Add(filmId); }

        // Видаляє фільм із фільмографії
        public void RemoveFilmography(int filmId) { if (FilmographyIds.Contains(filmId)) FilmographyIds.Remove(filmId); }

        // Повертає перші n фільмів з фільмографії
        public IEnumerable<int> GetTopFilms(int n) => FilmographyIds.Take(n);

        // Додає нагороду та підвищує популярність актора
        public void AddAward(string award) { if (!Awards.Contains(award)) Awards.Add(award); Popularity += 2; }

        // Обчислює кількість років досвіду у кіно
        public int CalculateExperienceYears() => Math.Max(0, DateTime.Now.Year - DebutYear);

        // Перевизначає метод і повертає коротку інформацію про актора
        public override string GetSummary() => $"{FullName} - Actor, Popularity: {Popularity}, Experience: {CalculateExperienceYears()} years";

        // Змінює доступність актора (доступний/недоступний)
        public void ToggleAvailability() { IsAvailable = !IsAvailable; }

        // Додає нову мову, якщо актор її ще не знає
        public void LearnLanguage(string lang) { if (!Languages.Contains(lang)) Languages.Add(lang); }

        // Видаляє мову зі списку
        public void RemoveLanguage(string lang) { if (Languages.Contains(lang)) Languages.Remove(lang); }
    }
}
