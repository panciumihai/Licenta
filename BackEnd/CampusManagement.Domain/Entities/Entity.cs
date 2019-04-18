using System;
using System.ComponentModel.DataAnnotations;

namespace CampusManagement.Entities
{
    public class Entity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public bool Available { get; set; } = true;

        public void Delete()
        {
            Available = false;
        }
    }
}
