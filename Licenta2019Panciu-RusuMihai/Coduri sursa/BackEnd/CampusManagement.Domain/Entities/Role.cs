using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CampusManagement.Domain.Entities
{
    public class Role : Entity
    {
        [Required]
        public string Name { get; private set; }

        public ICollection<PersonRole> PersonRoles { get; set; } = new Collection<PersonRole>();

        public static Role Create(string name) => new Role(){Name = name};
    }
}
