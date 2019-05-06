using System;
using System.ComponentModel.DataAnnotations;

namespace CampusManagement.Entities
{
    public class Entity
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public bool Available { get; set; } = true;

        public void Delete()
        {
            Available = false;
        }

        public void Initialize()
        {
            Id = Guid.NewGuid();
        }
    }
}
