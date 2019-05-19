using System;
using System.Collections.Generic;
using CampusManagement.Domain.Entities;

namespace CampusManagement.Business.Application.Models
{
    public class ApplicationDetailsModel
    {
        public bool ChildOfTeacher { get; set; }

        public string SpecialCases { get; set; }
        public bool AccommodationRequest { get; set; }
        public bool Scholarship { get; set; }
        public string LastYearLocation { get; set; }

        public ICollection<HostelPreferenceDetailsModel> HostelPreferences { get; set; }
        public DateTimeOffset PostedDateTime { get; set; }

        public Guid StudentId { get; set; }
        public Domain.Entities.Student Student { get; set; }
    }
}
