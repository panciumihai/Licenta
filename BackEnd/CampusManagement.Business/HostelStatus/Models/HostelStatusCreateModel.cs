using System;
using CampusManagement.Business.Hostel.Models;

namespace CampusManagement.Business.HostelStatus.Models
{
    public class HostelStatusCreateModel
    {
        public Guid HostelId { get; set; }

        public int MaleSeats { get; set; }
        public int ReservedMaleSeats { get; set; }

        public int FemaleSeats { get; set; }
        public int ReservedFemaleSeats { get; set; }
    }
}
