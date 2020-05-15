using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class AnimalType
    {
        [Key]
        public int AnimalTypeId { get; set; }
        public string Name { get; set; }
    }
}
