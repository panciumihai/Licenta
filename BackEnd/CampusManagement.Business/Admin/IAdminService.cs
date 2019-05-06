using CampusManagement.Business.Admin.Models;
using CampusManagement.Business.Generics;

namespace CampusManagement.Business.Admin
{
    public interface IAdminService : 
        IDetailsService<AdminDetailsModel>,
        ICreateService<AdminCreateModel>
    {
    }
}
