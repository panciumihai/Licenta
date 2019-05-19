using System;

namespace CampusManagement.Business.Application.Models
{
    public class HostelPreferenceDetailsModel
    {
        public int PreferenceNumber { get; set; }

        public Guid HostelId { get; set; }
        //public Domain.Entities.Hostel Hostel { get; set; }
    }
}
