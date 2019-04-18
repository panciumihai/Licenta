using CampusManagement.Business.Models;

namespace CampusManagement.Business.Student.Models
{
    public class StudentDetailsModel : PersonDetailsModel
    {
        public short Year { get; private set; }
    }
}
