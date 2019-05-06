using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Student.Models
{
    public class StudentDetailsModel : PersonDetailsModel
    {
        public short Year { get; private set; }
    }
}
