using System;
using CampusManagement.Entities;

namespace CampusManagement.Domain.Entities
{
    public class Student : Person
    {
        public short Year { get; private set; }

        public static Student Create(string firstName, string lastName,
            string email, string pass, string group, short year)
        {
            var student = (Student)Create(firstName, lastName,
                email, pass);
            student.Year = year;
            return student;
        }

        public void SetDefaults()
        {
            Id = Guid.NewGuid();
        }
    }
}
