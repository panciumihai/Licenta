using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Student.Models
{
    public class StudentCreateModel : PersonCreateModel
    {
        public short Year { get; set; }
    }
}
