using System;
using CampusManagement.Business.Hostel.Models;

namespace CampusManagement.Business.HostelStatus.Models
{
    public class HostelStatusCreateModel
    {
        public Guid HostelId { get; set; }

        public int MaleSeats { get; set; } = 0;
        public int ReservedMaleSeats { get; set; } = 0;

        public int FemaleSeats { get; set; } = 0;
        public int ReservedFemaleSeats { get; set; } = 0;
    }
}
