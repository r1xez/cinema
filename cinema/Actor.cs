using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    public class Actor : Customer
    {
        public List<int> FilmographyIds { get; set; }
        public List<string> Awards { get; set; }
        public string Agent { get; set; }
        public decimal HeightCm { get; set; }
        public decimal WeightKg { get; set; }
        public bool IsAvailable { get; set; }
        public double Popularity { get; set; }
        public int DebutYear { get; set; }
        public List<string> Languages { get; set; }
        public int AnnualShows { get; set; }

        public Actor()
        {
            Role = "Actor";
            FilmographyIds = new List<int>();
            Awards = new List<string>();
            Languages = new List<string>();
        }

        public void AddFilmography(int filmId) { if (!FilmographyIds.Contains(filmId)) FilmographyIds.Add(filmId); }

        public void RemoveFilmography(int filmId) { if (FilmographyIds.Contains(filmId)) FilmographyIds.Remove(filmId); }

        public IEnumerable<int> GetTopFilms(int n) => FilmographyIds.Take(n);

        public void AddAward(string award) { if (!Awards.Contains(award)) Awards.Add(award); Popularity += 2; }

        public int CalculateExperienceYears() => Math.Max(0, DateTime.Now.Year - DebutYear);

        public override string GetSummary() => $"{FullName} - Actor, Popularity: {Popularity}, Experience: {CalculateExperienceYears()} years";

        public void ToggleAvailability() { IsAvailable = !IsAvailable; }

        public void LearnLanguage(string lang) { if (!Languages.Contains(lang)) Languages.Add(lang); }

        public void RemoveLanguage(string lang) { if (Languages.Contains(lang)) Languages.Remove(lang); }
    }
}
