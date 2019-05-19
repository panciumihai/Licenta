using System;
using System.Threading.Tasks;
using CampusManagement.Business.Admin.Models;
using CampusManagement.Business.Generics;

namespace CampusManagement.Business.Admin
{
    public interface IAdminService : 
        IDetailsService<AdminDetailsModel>,
        ICreateService<AdminCreateModel>
    {
        Task<AdminDetailsModel> GetAdminByPersonId(Guid id, params string[] includes);
    }
}
