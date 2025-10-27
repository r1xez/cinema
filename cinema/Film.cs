using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    // Клас Film представляє фільм у системі кінотеатру
    public class Film
    {
        // Унікальний ідентифікатор фільму
        public int Id { get; set; }

        // Назва фільму
        public string Title { get; set; }

        // Назва жанру (наприклад: Action, Drama)
        public string GenreName { get; set; }

        // Опис жанру
        public string GenreDescription { get; set; }

        // Тривалість фільму у хвилинах
        public int DurationMinutes { get; set; }

        // Рейтинг фільму (від 0 до 10), оновлюється тільки через метод UpdateRating
        public double Rating { get; private set; }

        // Вікове обмеження (наприклад: 12+, 16+, 18+)
        public int AgeRestriction { get; set; }

        // Дата виходу фільму
        public DateTime ReleaseDate { get; set; }

        // Режисер фільму (ПІБ)
        public string Director { get; set; }

        // Країна виробництва
        public string Country { get; set; }

        // Коефіцієнт для ціни квитків на цей фільм
        public decimal PriceCoefficient { get; set; }

        // Акторський склад (список імен акторів)
        public List<string> Cast { get; set; }

        // Кількість голосів у рейтингу
        public int Votes { get; private set; }

        // Конструктор ініціалізує всі властивості фільму
        public Film(int id, string title, string genreName, string genreDescription, int durationMinutes, double rating, int ageRestriction, DateTime releaseDate, string director, string country, decimal priceCoefficient)
        {
            Id = id;
            Title = title;
            GenreName = genreName;
            GenreDescription = genreDescription;
            DurationMinutes = durationMinutes;
            Rating = rating;
            AgeRestriction = ageRestriction;
            ReleaseDate = releaseDate;
            Director = director;
            Country = country;
            PriceCoefficient = priceCoefficient;
            Cast = new List<string>();
            Votes = 0;
        }

        // Повертає текстову інформацію про фільм
        public string GetInfo()
        {
            return $"{Title} ({ReleaseDate.Year}) - {GenreName}, directed by {Director}. Rating: {Math.Round(Rating, 2)}/10, Duration: {DurationMinutes} mins, Age: {AgeRestriction}+";
        }

        // Повертає ідентифікатор фільму
        public int GetID() => Id;

        // Перевіряє чи дозволений фільм для глядача певного віку
        public bool IsSuitableForAge(int age) => age >= AgeRestriction;

        // Обчислення ціни квитка з урахуванням коефіцієнта
        public decimal TicketPrice(decimal basePrice) => Math.Max(0, basePrice * PriceCoefficient);

        // Оновлення рейтингу фільму з урахуванням нового голосу
        public void UpdateRating(double newRating)
        {
            if (newRating < 0 || newRating > 10) throw new ArgumentOutOfRangeException(); // перевірка валідності
            double total = Rating * Votes + newRating; // нова сумарна оцінка
            Votes++; // збільшуємо кількість голосів
            Rating = total / Votes; // перерахунок середнього
        }

        // Перевіряє чи фільм є новим (менше 30 днів з дати виходу)
        public bool IsNewRelease() => (DateTime.Now - ReleaseDate).TotalDays <= 30;

        // Додає актора до списку
        public void AddCastMember(string actor) { if (!Cast.Contains(actor)) Cast.Add(actor); }

        // Видаляє актора зі списку
        public void RemoveCastMember(string actor) { if (Cast.Contains(actor)) Cast.Remove(actor); }

        // Отримати перших n акторів із списку
        public IEnumerable<string> GetTopCast(int n) => Cast.Take(n);

        // Повертає скільки років фільму
        public int AgeInYears() => DateTime.Now.Year - ReleaseDate.Year;
    }
}
