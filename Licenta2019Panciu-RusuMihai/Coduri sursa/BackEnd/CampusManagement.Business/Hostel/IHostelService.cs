using CampusManagement.Business.Generics;
using CampusManagement.Business.Hostel.Models;

namespace CampusManagement.Business.Hostel
{
    public interface IHostelService : 
        IDetailsService<HostelDetailsModel>,
        ICreateService<HostelCreateModel>
    {
    }
}
