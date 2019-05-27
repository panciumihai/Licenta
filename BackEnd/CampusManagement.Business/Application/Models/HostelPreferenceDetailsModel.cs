using System;
using CampusManagement.Business.Hostel.Models;

namespace CampusManagement.Business.Application.Models
{
    public class HostelPreferenceDetailsModel
    {
        public int PreferenceNumber { get; set; }

        public Guid HostelId { get; set; }
        public HostelDetailsModel Hostel { get; set; }
    }
}
