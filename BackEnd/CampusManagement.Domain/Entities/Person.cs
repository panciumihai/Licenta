using System;
using System.ComponentModel.DataAnnotations;

namespace CampusManagement.Entities
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }

        public static Person Create(string firstName, string lastName,
            string email, string password) => new Person()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
            };

        public static Person Create(Person person) => new Person()
        {
            Id = Guid.NewGuid(),
            FirstName = person.FirstName,
            LastName = person.LastName,
            Email = person.Email,
            Password = person.Password
        };
    }
}
