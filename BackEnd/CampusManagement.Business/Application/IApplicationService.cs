using CampusManagement.Business.Application.Models;
using CampusManagement.Business.Generics;

namespace CampusManagement.Business.Application
{
    public interface IApplicationService:
        IDetailsService<ApplicationDetailsModel>,
        ICreateService<ApplicationCreateModel>
    {
    }
}
