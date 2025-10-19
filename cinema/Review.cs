using System;

namespace cinema
{
    public class Review
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int FilmId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; private set; }
        public int HelpfulCount { get; private set; }
        public bool HasSpoiler { get; set; }
        public string Language { get; set; }
        public int Likes { get; private set; }

        public Review()
        {
            CreatedAt = DateTime.Now;
            IsApproved = false;
        }

        public void Approve() { IsApproved = true; }

        public void Edit(string newComment, int newRating)
        {
            Comment = newComment;
            Rating = Math.Clamp(newRating, 1, 10);
            CreatedAt = DateTime.Now;
            IsApproved = false;
        }

        public void Upvote() { Likes++; HelpfulCount++; }

        public void Downvote() { if (HelpfulCount > 0) HelpfulCount--; }

        public string Summarize() => $"{Rating}/10 - {(Comment ?? "").Substring(0, Math.Min(80, Comment?.Length ?? 0))}";

        public void MarkSpoiler(bool spoiler) { HasSpoiler = spoiler; }
    }
}
