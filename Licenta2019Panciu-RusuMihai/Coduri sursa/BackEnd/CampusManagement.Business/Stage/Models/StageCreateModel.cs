﻿using System;

namespace CampusManagement.Business.Stage.Models
{
    public class StageCreateModel
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get;  set; }
        public string Details { get; set; }
    }
}
