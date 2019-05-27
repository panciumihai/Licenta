﻿using System;
using System.Collections.Generic;
using CampusManagement.Business.Hostel.Models;
using CampusManagement.Domain.Entities;

namespace CampusManagement.Business.HostelStatus.Models
{
    public class HostelStatusDetailsModel
    {
        public Guid HostelId { get; private set; }
        public HostelDetailsModel Hostel { get; private set; }

        public int MaleSeats { get; private set; }
        public int OccupiedMaleSeats { get; private set; }
        public int ReservedMaleSeats { get; private set; }

        public int FemaleSeats { get; private set; }
        public int OccupiedFemaleSeats { get; private set; }
        public int ReservedFemaleSeats { get; private set; }


        public ICollection<StudentsGroup> StudentsGroups { get; private set; }

        public DateTimeOffset CreatedDateTime { get; private set; }
    }
}
