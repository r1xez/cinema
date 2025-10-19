using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Popularity { get; private set; }
        public int MinAge { get; set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int TypicalDuration { get; set; }
        public string ExampleFilm { get; set; }
        public List<int> FilmIds { get; set; }
        public string RegionPopularity { get; set; }

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
            FilmIds = new List<int>();
            RegionPopularity = "Global";
        }

        public string GetInfo()
        {
            return $"{Name} - {Description}. Popularity: {Popularity}/100, Min Age: {MinAge}+";
        }

        public bool IsSuitableForChild(int age) => age >= MinAge;

        public void UpdatePopularity(int change)
        {
            Popularity = Math.Clamp(Popularity + change, 0, 100);
            UpdatedAt = DateTime.Now;
        }

        public void Deactivate() { IsActive = false; UpdatedAt = DateTime.Now; }

        public void UpdateDescription(string newDescription) { Description = newDescription; UpdatedAt = DateTime.Now; }

        public void AddFilm(int filmId) { if (!FilmIds.Contains(filmId)) FilmIds.Add(filmId); }

        public void RemoveFilm(int filmId) { if (FilmIds.Contains(filmId)) FilmIds.Remove(filmId); }

        public double AverageDurationEstimate() => TypicalDuration;

        public void SetRegionPopularity(string region) { RegionPopularity = region; UpdatedAt = DateTime.Now; }
    }
}
