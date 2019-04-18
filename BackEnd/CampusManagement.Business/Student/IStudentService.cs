using CampusManagement.Business.Generics;
using CampusManagement.Business.Student.Models;

namespace CampusManagement.Business.Student
{
    public interface IStudentService : 
        IDetailsService<StudentDetailsModel>, 
        ICreateService<StudentCreateModel>
    {
    }
}
