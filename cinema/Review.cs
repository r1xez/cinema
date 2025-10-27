using System;

namespace cinema
{
    // Клас відгуку клієнта про фільм
    public class Review
    {
        public int Id { get; set; }                // Унікальний ідентифікатор відгуку
        public int CustomerId { get; set; }        // Ідентифікатор клієнта, який залишив відгук
        public int FilmId { get; set; }            // Ідентифікатор фільму, до якого відгук
        public int Rating { get; set; }            // Оцінка фільму (1-10)
        public string Comment { get; set; }        // Текстовий коментар клієнта
        public DateTime CreatedAt { get; set; }    // Дата створення або останнього редагування
        public bool IsApproved { get; private set; } // Прапорець, чи відгук затверджено модератором
        public int HelpfulCount { get; private set; } // Кількість користувачів, яким відгук був корисний
        public bool HasSpoiler { get; set; }       // Чи містить відгук спойлери
        public string Language { get; set; }       // Мова відгуку
        public int Likes { get; private set; }     // Кількість лайків відгуку

        // Конструктор: ініціалізація дати створення та статусу модерації
        public Review()
        {
            CreatedAt = DateTime.Now;
            IsApproved = false; // За замовчуванням відгук не затверджений
        }

        // Затвердження відгуку модератором
        public void Approve() { IsApproved = true; }

        // Редагування відгуку: оновлення коментаря та рейтингу
        public void Edit(string newComment, int newRating)
        {
            Comment = newComment;
            Rating = Math.Clamp(newRating, 1, 10); // Обмеження рейтингу від 1 до 10
            CreatedAt = DateTime.Now;               // Оновлення дати редагування
            IsApproved = false;                     // Після редагування потрібно повторне затвердження
        }

        // Додавання лайку/позитивної оцінки від інших користувачів
        public void Upvote() { Likes++; HelpfulCount++; }

        // Зменшення користі відгуку (якщо користувач відмінив корисність)
        public void Downvote() { if (HelpfulCount > 0) HelpfulCount--; }

        // Короткий опис відгуку: перші 80 символів коментаря + рейтинг
        public string Summarize() => $"{Rating}/10 - {(Comment ?? "").Substring(0, Math.Min(80, Comment?.Length ?? 0))}";

        // Встановлення прапорця спойлера
        public void MarkSpoiler(bool spoiler) { HasSpoiler = spoiler; }
    }
}
