using System;
using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Admin.Models
{
    public class AdminDetailsModel : PersonDetailsModel
    {
        public Guid Id { get; set; }
    }
}
