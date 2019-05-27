﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using CampusManagement.Entities;

namespace CampusManagement.Domain.Entities
{
    public class StudentsGroup : Entity
    {
        public string Gender { get; private set; }
        public string Year { get; private set; }

        public ICollection<Student> Students { get; private set; } = new Collection<Student>();

        public static StudentsGroup Create(string gender, string year, ICollection<Student> students)
        => new StudentsGroup()
        {
            Gender = gender,
            Year = year,
            Students = students
        };
    }
}
