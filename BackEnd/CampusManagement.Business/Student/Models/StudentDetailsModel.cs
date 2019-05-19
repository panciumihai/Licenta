using System;
using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Student.Models
{
    public class StudentDetailsModel : PersonDetailsModel
    {
        public Guid Id { get; private set; }
        public short Year { get; private set; }
    }
}
