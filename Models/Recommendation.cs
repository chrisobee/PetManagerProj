using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetManager.Models
{
    public class Recommendation
    {
        [Key]
        public int RecommendationId { get; set; }
        public string TaskName { get; set; }
        public string Interval { get; set; }
        [ForeignKey("AnimalType")]
        public int? AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}
