using System;
using CampusManagement.Entities;

namespace CampusManagement.Domain.Entities
{
    public class HostelPreference : Entity
    {
        public int PreferenceNumber { get; private set; }

        public Guid HostelId { get; private set; }
        public Hostel Hostel { get; private set; }

        public Guid ApplicationId { get;  set; }
        public Application Application { get;  set; }

        public static HostelPreference Create(int preferenceNumber, Hostel hostel, Application application)
            => new HostelPreference()
            {
                PreferenceNumber = preferenceNumber,
                HostelId = hostel.Id,
                Hostel = hostel,
                ApplicationId = application.Id,
                Application = application
            };
    }
}
