using System;
using System.ComponentModel.DataAnnotations.Schema;
using CampusManagement.Entities;

namespace CampusManagement.Domain.Entities
{
    [Table("PersonRoles")]
    public class PersonRole : Entity
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
