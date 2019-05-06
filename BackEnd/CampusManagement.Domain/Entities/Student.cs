using System;
using CampusManagement.Entities;

namespace CampusManagement.Domain.Entities
{
    public class Student : Entity
    {
        public short Year { get; private set; }

        public Guid PersonId { get; private set; }
        public Person Person { get; private set; }

        public static Student Create(Person person, short year)
        {
            var student = new Student
            {
                Person = person,
                PersonId = person.Id,
                Year = year
            };

            return student;
        }
    }
}
