using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [ForeignKey("AnimalType")]
        public int AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; }
        [NotMapped]
        public List<Recommendation> Recommendations { get; set; }
        public string PhotoPath { get; set; }
    }
}
