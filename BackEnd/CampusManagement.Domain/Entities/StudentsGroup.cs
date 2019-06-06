﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CampusManagement.Entities;

namespace CampusManagement.Domain.Entities
{
    public class StudentsGroup : Entity
    {
        public Guid HostelStatusId { get; private set; }

        public string Gender { get; private set; }
        public int Year { get; private set; }

        public int Seats { get; private set; }

        public ICollection<Student> Students { get; private set; } = new Collection<Student>();

        public static StudentsGroup Create(Guid hostelStatusId, string gender, int year, int seats, ICollection<Student> students)
        => new StudentsGroup()
        {
            HostelStatusId = hostelStatusId,
            Gender = gender,
            Year = year,
            Seats = seats,
            Students = students
        };
    }
}
