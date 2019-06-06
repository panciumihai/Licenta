using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.Generics;
using CampusManagement.Business.HostelStatus.Models;

namespace CampusManagement.Business.HostelStatus
{
    public interface IHostelStatusService:
        IDetailsService<HostelStatusDetailsModel>,
        ICreateService<HostelStatusCreateModel>
    {
        Task<Guid> AddOrUpdate(HostelStatusCreateModel hostelStatusCreateModel);
        Task<IEnumerable<Guid>> AddOrUpdate(IEnumerable<HostelStatusCreateModel> hostelStatusCreateModel);

        Task<IEnumerable<HostelStatusDetailsModel>> GetSeats();
        Task<IEnumerable<HostelStatusDetailsModel>> SeatsAllocationPreview();
    }
}
