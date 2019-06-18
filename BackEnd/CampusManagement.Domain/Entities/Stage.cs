﻿using System;

namespace CampusManagement.Domain.Entities
{
    public class Stage : Entity
    {
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public DateTimeOffset PostedDate { get; private set; } = DateTimeOffset.Now;
        public string Details { get; private set; }

        public static Stage Create(DateTimeOffset startDate, DateTimeOffset endDate, string details) => new Stage()
        {
            StartDate = startDate,
            EndDate = endDate,
            Details = details
        };
    }
}
