using System;
using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Student.Models
{
    public class StudentDetailsModel : PersonDetailsModel
    {
        public Guid Id { get; private set; }
        public short Year { get; private set; }

        public string Cnp { get; set; }
        public string Nationality { get; set; }

        public double Score { get; set; }
        public double SecondScore { get; set; }
    }
}
