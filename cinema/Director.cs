using System;
using System.Collections.Generic;
using System.Linq;

namespace cinema
{
    public class Director : Customer
    {
        public List<int> DirectedFilmIds { get; set; }
        public List<string> Awards { get; set; }
        public string Style { get; set; }
        public decimal AvgBudget { get; set; }
        public string AgentContact { get; set; }
        public bool IsActiveDirector { get; set; }
        public int YearsActive { get; set; }
        public List<string> NotableWorks { get; set; }
        public int AwardsCount { get; set; }
        public double Rating { get; set; }

        public Director()
        {
            Role = "Director";
            DirectedFilmIds = new List<int>();
            Awards = new List<string>();
            NotableWorks = new List<string>();
        }

        public void AddDirectedFilm(int filmId) { if (!DirectedFilmIds.Contains(filmId)) DirectedFilmIds.Add(filmId); }

        public void RemoveDirectedFilm(int filmId) { if (DirectedFilmIds.Contains(filmId)) DirectedFilmIds.Remove(filmId); }

        public int GetFilmCount() => DirectedFilmIds.Count;

        public decimal CalculateAvgFilmDuration(List<Film> films)
        {
            var directed = films.Where(f => DirectedFilmIds.Contains(f.Id)).ToList();
            if (!directed.Any()) return 0;
            return (decimal)directed.Average(f => f.DurationMinutes);
        }

        public void AwardPrize(string prize) { Awards.Add(prize); AwardsCount++; Rating += 0.5; }

        public override string GetSummary() => $"{FullName} - Director, Films: {GetFilmCount()}, Rating: {Rating}";

        public void SetActive(bool active) { IsActiveDirector = active; }

        public void AddNotableWork(string work) { if (!NotableWorks.Contains(work)) NotableWorks.Add(work); }
    }
}
