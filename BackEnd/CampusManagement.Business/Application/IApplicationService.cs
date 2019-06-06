using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.Application.Models;
using CampusManagement.Business.Generics;
using CampusManagement.Business.HostelStatus.Models;

namespace CampusManagement.Business.Application
{
    public interface IApplicationService:
        IDetailsService<ApplicationDetailsModel>,
        ICreateService<ApplicationCreateModel>
    {
        Task<IEnumerable<StudentsYearDistribution>> SeatsDistribution();
    }
}
