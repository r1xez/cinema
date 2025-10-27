using System;
using System.Collections.Generic;

namespace cinema
{
    // Абстрактний клас Person — базовий клас для всіх людей у системі
    public abstract class Person
    {
        // Унікальний ідентифікатор особи
        public int Id { get; set; }

        // Ім'я
        public string FirstName { get; set; }

        // Прізвище
        public string LastName { get; set; }

        // Повне ім'я (поєднання FirstName та LastName)
        public string FullName => $"{FirstName} {LastName}";

        // Дата народження
        public DateTime BirthDate { get; set; }

        // Електронна пошта
        public string Email { get; set; }

        // Телефон
        public string Phone { get; set; }

        // Коротка біографія
        public string Bio { get; set; }

        // Національність
        public string Nationality { get; set; }

        // Роль особи (Customer, Staff, Actor тощо)
        public string Role { get; protected set; }

        // Додаткові метадані у вигляді ключ-значення
        public Dictionary<string, string> Metadata { get; set; }

        // Конструктор — ініціалізує словник Metadata
        public Person()
        {
            Metadata = new Dictionary<string, string>();
        }

        // Обчислення віку на основі дати народження
        public int GetAge()
        {
            var age = DateTime.Now.Year - BirthDate.Year;
            if (BirthDate > DateTime.Now.AddYears(-age)) age--;
            return age;
        }

        // Повертає контактну інформацію (ім'я, email, телефон)
        public string GetContactInfo() => $"{FullName} | Email: {Email} | Phone: {Phone}";

        // Оновлення email
        public void UpdateEmail(string newEmail) { Email = newEmail; }

        // Оновлення телефону
        public void UpdatePhone(string newPhone) { Phone = newPhone; }

        // Повертає короткий опис особи
        public virtual string GetSummary() => $"{FullName} ({Role}) - {Nationality}";

        // Додавання або оновлення метаданих
        public void AddMetadata(string key, string value) { Metadata[key] = value; }

        // Видалення метаданих за ключем
        public bool RemoveMetadata(string key) => Metadata.Remove(key);
    }
}
