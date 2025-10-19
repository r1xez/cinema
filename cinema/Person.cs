using System;
using System.Collections.Generic;

namespace cinema
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string Nationality { get; set; }
        public string Role { get; protected set; }
        public Dictionary<string, string> Metadata { get; set; }

        public Person()
        {
            Metadata = new Dictionary<string, string>();
        }

        public int GetAge()
        {
            var age = DateTime.Now.Year - BirthDate.Year;
            if (BirthDate > DateTime.Now.AddYears(-age)) age--;
            return age;
        }

        public string GetContactInfo() => $"{FullName} | Email: {Email} | Phone: {Phone}";

        public void UpdateEmail(string newEmail) { Email = newEmail; }

        public void UpdatePhone(string newPhone) { Phone = newPhone; }

        public virtual string GetSummary() => $"{FullName} ({Role}) - {Nationality}";

        public void AddMetadata(string key, string value) { Metadata[key] = value; }

        public bool RemoveMetadata(string key) => Metadata.Remove(key);
    }
}
