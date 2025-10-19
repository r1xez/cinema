using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string GenreName { get; set; }
        public string GenreDescription { get; set; }
        public int DurationMinutes { get; set; }
        public double Rating { get; private set; }
        public int AgeRestriction { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Country { get; set; }
        public decimal PriceCoefficient { get; set; }
        public List<string> Cast { get; set; }
        public int Votes { get; private set; }

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

        public string GetInfo()
        {
            return $"{Title} ({ReleaseDate.Year}) - {GenreName}, directed by {Director}. Rating: {Math.Round(Rating, 2)}/10, Duration: {DurationMinutes} mins, Age: {AgeRestriction}+";
        }

        public int GetID() => Id;

        public bool IsSuitableForAge(int age) => age >= AgeRestriction;

        public decimal TicketPrice(decimal basePrice) => Math.Max(0, basePrice * PriceCoefficient);

        public void UpdateRating(double newRating)
        {
            if (newRating < 0 || newRating > 10) throw new ArgumentOutOfRangeException();
            double total = Rating * Votes + newRating;
            Votes++;
            Rating = total / Votes;
        }

        public bool IsNewRelease() => (DateTime.Now - ReleaseDate).TotalDays <= 30;

        public void AddCastMember(string actor) { if (!Cast.Contains(actor)) Cast.Add(actor); }

        public void RemoveCastMember(string actor) { if (Cast.Contains(actor)) Cast.Remove(actor); }

        public IEnumerable<string> GetTopCast(int n) => Cast.Take(n);

        public int AgeInYears() => DateTime.Now.Year - ReleaseDate.Year;
    }
}
